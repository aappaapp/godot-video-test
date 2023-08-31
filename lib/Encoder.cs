using FFmpeg.AutoGen.Abstractions;
using Godot;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Encoder : IDisposable
{
  public AVCodec* Codec;
  public AVCodecContext* Context;

  public Encoder(int p_width, int p_height, int p_framerate)
  {
    int ret;

    Codec = ffmpeg.avcodec_find_encoder(AVCodecID.AV_CODEC_ID_HEVC);
    Context = ffmpeg.avcodec_alloc_context3(Codec);
    Context->height = p_height;
    Context->width = p_width;
    Context->bit_rate = 480000;
    Context->framerate = new() { num = p_framerate, den = 1 };
    Context->time_base = new() { num = 1, den = p_framerate };
    Context->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;

    ret = ffmpeg.avcodec_open2(Context, Codec, null);
    if (ret < 0)
      throw new Exception($"Could not open codec: {ret}");
  }

  public int RecievePacket(Packet p_packet)
  {
    return ffmpeg.avcodec_receive_packet(Context, p_packet.Pointer);
  }

  public int SendFrame(Frame p_frame)
  {
    return ffmpeg.avcodec_send_frame(Context, p_frame.Pointer);
  }

  public void Dispose()
  {
    fixed (AVCodecContext** p_Context = &Context)
      ffmpeg.avcodec_free_context(p_Context);
  }
}
