using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVFormatContext
{
  public AutoGen.Abstractions.AVFormatContext* Pointer;

  public AVFormatContext(AutoGen.Abstractions.AVFormatContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVFormatContext()
  {
    ffmpeg.avformat_free_context(Pointer);
  }

  public int AllocOutputContext(AVOutputFormat output_format, string format_name = null, string filename = null)
  {
    fixed (AutoGen.Abstractions.AVFormatContext** ptr_Pointer = &Pointer)
    {
      return ffmpeg.avformat_alloc_output_context2(ptr_Pointer, output_format.Pointer, format_name, filename);
    }
  }

  public static AVFormatContext AllocContext()
  {
    return new(ffmpeg.avformat_alloc_context());
  }

}
