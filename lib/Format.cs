using FFmpeg.AutoGen.Abstractions;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Format : IDisposable
{
  public AVFormatContext* Pointer;

  public Format()
  {
    Pointer = ffmpeg.avformat_alloc_context();
  }

  public int WriteFrame(Packet p_packet)
  {
    return ffmpeg.av_write_frame(Pointer, p_packet.Pointer);
  }

  public void Dispose()
  {
    ffmpeg.avformat_free_context(Pointer);
  }
}
