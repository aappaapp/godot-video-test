using System;

namespace FFmpeg.AdvanceWrapper;

public class FFmpegException : Exception
{
  public int Code;

  public FFmpegException(int p_errorCode, string p_message = "") : base($"Error: {p_errorCode}{(p_message != "" ? $" Message: {p_message}" : "")}")
  {
    Code = Math.Abs(p_errorCode);
  }

  public static void ThrowIfLessThanZero(int p_returnValue, string p_message = "")
  {
    if (p_returnValue < 0) throw new FFmpegException(p_returnValue, p_message);
  }
}
