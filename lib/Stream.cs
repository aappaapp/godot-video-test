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

  public void Dispose() { }
}
