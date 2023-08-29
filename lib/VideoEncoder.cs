using System;
using System.IO;
using System.Runtime.InteropServices;
using FFmpeg.AutoGen.Abstractions;
using Godot;

namespace GodotFFmpegTest;

public unsafe class VideoEncoder : IDisposable
{
  public AVCodec* codec = null;
  public AVCodecContext* codecContext = null;
  public AVStream* stream = null;
  public AVFormatContext* formatContext = null;
  public SwsContext* swsContext = null;
  public AVFrame* frame = null;

  public int width;
  public int height;

  public VideoEncoder(int p_width, int p_height)
  {
    height = p_height;
    width = p_width;

    codec = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_MPEG4);
    codecContext = ffmpeg.avcodec_alloc_context3(codec);
    codecContext->height = height;
    codecContext->width = width;
    codecContext->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;
    ffmpeg.avcodec_open2(codecContext, codec, null);

    fixed (AVFormatContext** formatContextPtr = &formatContext)
      ffmpeg.avformat_alloc_output_context2(formatContextPtr, null, "mp4", null);
    stream = ffmpeg.avformat_new_stream(formatContext, null);

    frame = ffmpeg.av_frame_alloc();
    frame->format = (int)AVPixelFormat.AV_PIX_FMT_RGBA;
    frame->height = height;
    frame->width = width;

    swsContext = ffmpeg.sws_getContext(width, height, AVPixelFormat.AV_PIX_FMT_RGBA, width, height, AVPixelFormat.AV_PIX_FMT_YUV420P, ffmpeg.SWS_FAST_BILINEAR, null, null, null);

  }

  public void SendImage(Image image)
  {
    byte[] image_buffer = image.SavePngToBuffer();
    byte_ptr8 ptr = new byte_ptr8();
    AVPacket* packet = ffmpeg.av_packet_alloc();
    fixed (byte* a = image_buffer)
      ptr[0] = a;

    frame->data = ptr;
    ffmpeg.avcodec_send_frame(codecContext, frame);
    int ret = ffmpeg.avcodec_receive_packet(codecContext, packet);
    GD.Print(ret);
  }

  public void Get()
  {
  }

  public void Dispose()
  {
    var l_codecContext = codecContext;
    ffmpeg.avcodec_free_context(&l_codecContext);
  }
}
