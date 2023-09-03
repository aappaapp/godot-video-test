using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVStream
{
  public AutoGen.Abstractions.AVStream* Pointer;

  public int Id { get => Pointer->id; set => Pointer->id = value; }

  public AVStream(AutoGen.Abstractions.AVStream* p_pointer)
  {
    Pointer = p_pointer;
  }
}
