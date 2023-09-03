using FFmpeg.AutoGen.Abstractions;
using Godot;

namespace FFmpeg.Wrapper;

public unsafe class AVIOContext
{
  public AutoGen.Abstractions.AVIOContext* Pointer;

  public AVIOContext(AutoGen.Abstractions.AVIOContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVIOContext()
  {
    fixed (AutoGen.Abstractions.AVIOContext** ptr_Pointer = &Pointer)
      ffmpeg.avio_context_free(ptr_Pointer);
  }
}
