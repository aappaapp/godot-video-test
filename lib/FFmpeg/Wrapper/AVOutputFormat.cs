using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVOutputFormat
{
  public AutoGen.Abstractions.AVOutputFormat* Pointer;

  public AVOutputFormat(AutoGen.Abstractions.AVOutputFormat* p_pointer)
  {
    Pointer = p_pointer;
  }

  public static AVOutputFormat GuessFormat(string p_short_name, string p_filename, string p_mime_type)
  {
    return new(ffmpeg.av_guess_format(p_short_name, p_filename, p_mime_type));
  }
}
