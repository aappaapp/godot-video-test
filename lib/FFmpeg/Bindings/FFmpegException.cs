using System;
using FFmpeg.AutoGen;

namespace FFmpeg.Bindings;

public class FFmpegException : Exception
{
  public FFmpegException(int p_error_code, string p_message = "") : base($"Error: {p_error_code}{(p_message != "" ? $" Message: {p_message}" : "")}") { }

  public static void ThrowIfNotZero(int ret)
  {
    if (ret != 0) throw new FFmpegException(ret);
  }
}
