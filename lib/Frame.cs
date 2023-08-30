using FFmpeg.AutoGen.Abstractions;
using Godot;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Frame : IDisposable
{
  public AVFrame* Pointer;

  public Frame(Image image)
  {
    int ret;

    Pointer = ffmpeg.av_frame_alloc();
    Pointer->format = (int)AVPixelFormat.AV_PIX_FMT_YUV420P;
    Pointer->height = image.GetHeight();
    Pointer->width = image.GetWidth();

    ret = ffmpeg.av_frame_get_buffer(Pointer, 0);
    if (ret < 0)
      throw new Exception("Could not allocate the video frame data");

    ret = ffmpeg.av_frame_make_writable(Pointer);
    if (ret < 0)
      throw new Exception();

    for (int y = 0; y < image.GetHeight(); y++)
    {
      for (int x = 0; x < image.GetWidth(); x++)
      {
        Pointer->data[0][x * 3 + y * Pointer->linesize[0]] = (byte)image.GetPixel(x, y).R8;
        Pointer->data[0][x * 3 + y * Pointer->linesize[0] + 1] = (byte)image.GetPixel(x, y).G8;
        Pointer->data[0][x * 3 + y * Pointer->linesize[0] + 2] = (byte)image.GetPixel(x, y).B8;
      }
    }

    AVFrame* Pointer2 = ffmpeg.av_frame_alloc();

    SwsContext* swsContext = ffmpeg.sws_getContext(image.GetWidth(), image.GetHeight(), (AVPixelFormat)Pointer->format, image.GetWidth(), image.GetHeight(), AVPixelFormat.AV_PIX_FMT_YUV420P, 0, null, null, null);
    ffmpeg.sws_scale(swsContext, Pointer->data, Pointer->linesize, 0, Pointer2->height, Pointer2->data, Pointer2->linesize);
    Pointer = Pointer2;
  }

  public void Dispose()
  {
    fixed (AVFrame** p_Pointer = &Pointer)
      ffmpeg.av_frame_free(p_Pointer);
  }
}
