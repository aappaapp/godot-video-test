using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVPacket
{
  public AutoGen.Abstractions.AVPacket* Pointer;

  public AVPacket(AutoGen.Abstractions.AVPacket* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVPacket()
  {
    fixed (AutoGen.Abstractions.AVPacket** ptr_Pointer = &Pointer)
      ffmpeg.av_packet_free(ptr_Pointer);
  }

  public static AVPacket Alloc()
  {
    return new(ffmpeg.av_packet_alloc());
  }
}
