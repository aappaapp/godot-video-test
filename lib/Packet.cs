using FFmpeg.AutoGen.Abstractions;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Packet : IDisposable
{
  public AVPacket* Pointer;

  public Packet()
  {
    Pointer = ffmpeg.av_packet_alloc();
  }

  public void UnRef()
  {
    ffmpeg.av_packet_unref(Pointer);
  }

  public void Dispose()
  {
    fixed (AVPacket** p_Pointer = &Pointer)
      ffmpeg.av_packet_free(p_Pointer);
  }
}
