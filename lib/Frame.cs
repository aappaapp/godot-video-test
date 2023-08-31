using FFmpeg.AutoGen.Abstractions;
using Godot;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Frame : IDisposable
{
  public AVFrame* Pointer;

  public int Height { get { return Pointer->height; } }
  public int Width { get { return Pointer->width; } }

  public Frame(AVFrame* frame)
  {
    Pointer = frame;
  }

  public Frame(int width, int height, byte_ptr8 data = new())
  {
    Pointer = ffmpeg.av_frame_alloc();
    Pointer->format = (int)AVPixelFormat.AV_PIX_FMT_RGB24;
    Pointer->height = height;
    Pointer->width = width;
    Pointer->data = data;
  }

  public Frame ToYUV()
  {
    int ret;

    Frame yuvFrame = Clone();
    yuvFrame.Pointer->format = (int)AVPixelFormat.AV_PIX_FMT_YUV420P;
    yuvFrame.GetBuffer();
    yuvFrame.MakeWritable();

    SwsContext* swsContext = ffmpeg.sws_getContext(Width, Height, AVPixelFormat.AV_PIX_FMT_RGB24, Width, Height, AVPixelFormat.AV_PIX_FMT_YUV420P, 0, null, null, null);

    ret = ffmpeg.sws_scale(swsContext, Pointer->data, Pointer->linesize, 0, Height, yuvFrame.Pointer->data, yuvFrame.Pointer->linesize);
    if (ret < 0)
      throw new Exception(ret.ToString());

    Dispose();

    return yuvFrame;
  }

  public Frame Clone()
  {
    return new(Width, Height);
  }

  public void Dispose()
  {
    Free();
  }

  public void Free()
  {
    fixed (AVFrame** p_Pointer = &Pointer)
      ffmpeg.av_frame_free(p_Pointer);
  }

  public void GetBuffer()
  {
    int ret = ffmpeg.av_frame_get_buffer(Pointer, 0);
    if (ret < 0)
      throw new Exception("Could not allocate the video frame data");
  }

  public void MakeWritable()
  {
    int ret = ffmpeg.av_frame_make_writable(Pointer);
    if (ret < 0)
      throw new Exception();
  }

  public static Frame FromGodotImage(Image image)
  {
    using Frame frame = new(image.GetWidth(), image.GetHeight());

    frame.GetBuffer();
    frame.MakeWritable();

    for (int y = 0; y < frame.Height; y++)
    {
      for (int x = 0; x < frame.Width; x++)
      {
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0]] = (byte)image.GetPixel(x, y).R8;
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0] + 1] = (byte)image.GetPixel(x, y).G8;
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0] + 2] = (byte)image.GetPixel(x, y).B8;
      }
    }

    return frame.ToYUV();
  }
}
