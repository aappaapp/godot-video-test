using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVPacket
{
  public AutoGen.Abstractions.AVPacket* Pointer;

  public int StreamIndex { get => Pointer->stream_index; set => Pointer->stream_index = value; }

  public AVPacket(AutoGen.Abstractions.AVPacket* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVPacket()
  {
    fixed (AutoGen.Abstractions.AVPacket** ptr_Pointer = &Pointer)
      ffmpeg.av_packet_free(ptr_Pointer);
  }

  public void UnRef()
  {
    ffmpeg.av_packet_unref(Pointer);
  }

  public static AVPacket Alloc()
  {
    return new(ffmpeg.av_packet_alloc());
  }
}
