using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVIOBuffer
{
  public byte* Pointer;
  public ulong Size;

  public AVIOBuffer(byte* p_pointer, ulong p_size)
  {
    Pointer = p_pointer;
    Size = p_size;
  }

  ~AVIOBuffer()
  {
    ffmpeg.av_free(Pointer);
  }

  public static AVIOBuffer Malloc(ulong p_size)
  {
    return new((byte*)ffmpeg.av_malloc(p_size), p_size);
  }
}
