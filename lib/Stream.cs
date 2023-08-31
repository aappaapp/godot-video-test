using FFmpeg.AutoGen.Abstractions;
using System;

namespace FFmpeg.Wrapper;

public unsafe class Stream : IDisposable
{
  public AVStream* Pointer;

  public Stream(AVStream* stream)
  {
    Pointer = stream;
  }

  public Stream(FormatContext format)
  {
    Pointer = ffmpeg.avformat_new_stream(format.Pointer, null);
  }

  public void A() { }

  public void Dispose() { }
}
