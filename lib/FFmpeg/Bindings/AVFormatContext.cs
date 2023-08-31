using Godot;

namespace FFmpeg.Bindings;

public unsafe class AVFormatContext
{
  public AutoGen.AVFormatContext* Pointer;

  public AVFormatContext(AutoGen.AVFormatContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVFormatContext()
  {
    AutoGen.ffmpeg.avformat_free_context(Pointer);
  }

  public void AllocOutputContext(AVOutputFormat output_format, string format_name = null, string filename = null)
  {
    fixed (AutoGen.AVFormatContext** ptr_Pointer = &Pointer)
    {
      FFmpegException.ThrowIfNotZero(AutoGen.ffmpeg.avformat_alloc_output_context2(ptr_Pointer, output_format.Pointer, format_name, filename));
    }
  }

  public static AVFormatContext AllocContext()
  {
    return new(AutoGen.ffmpeg.avformat_alloc_context());
  }

}
