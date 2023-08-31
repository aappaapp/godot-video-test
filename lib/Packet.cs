using FFmpeg.AutoGen.Abstractions;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Packet : IDisposable
{
  public AVPacket* Pointer;

  public Packet()
  {
    Pointer = ffmpeg.av_packet_alloc();
    if (Pointer == null)
      throw new Exception("Could not allocate AVPacket");
    ffmpeg.av_new_packet(Pointer, 100);
  }

  public void Dispose()
  {
    Free();
  }

  public void Free()
  {
    fixed (AVPacket** p_Pointer = &Pointer)
      ffmpeg.av_packet_free(p_Pointer);
  }

  public void RescaleTs(AVRational p_src, AVRational p_dest)
  {
    ffmpeg.av_packet_rescale_ts(Pointer, p_src, p_dest);
  }

  public void UnRef()
  {
    ffmpeg.av_packet_unref(Pointer);
  }
}
