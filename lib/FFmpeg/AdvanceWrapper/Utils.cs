using FFmpeg.Wrapper;
using Godot;

namespace FFmpeg.AdvanceWrapper;

public static class Utils
{
  public static int AVERROR(this int p_code)
  {
    return AutoGen.Abstractions.ffmpeg.AVERROR(p_code);
  }

  public static unsafe void SetFramerate(this AVCodecContext p_context, int p_framerate)
  {
    p_context.Pointer->framerate = new() { num = p_framerate, den = 1 };
    p_context.Pointer->time_base = new() { num = 1, den = p_framerate };
  }

  public static unsafe void SetResolution(this AVCodecContext p_context, int p_width, int p_height)
  {
    p_context.Pointer->height = p_height;
    p_context.Pointer->width = p_width;
  }

  public static void ThrowIfLessThanZero(this int p_returnValue, string p_message = "")
  {
    FFmpegException.ThrowIfLessThanZero(p_returnValue, p_message);
  }

  public static unsafe AVFrame ToYUV(this AVFrame p_frame)
  {
    AVFrame yuvFrame = AVFrame.Allocate();

    yuvFrame.Format = AutoGen.Abstractions.AVPixelFormat.AV_PIX_FMT_YUV420P;
    yuvFrame.Height = p_frame.Height;
    yuvFrame.Width = p_frame.Width;

    yuvFrame.GetBuffer();
    yuvFrame.MakeWritable();

    SwsContext.GetContext(p_frame, yuvFrame).SwsScale(p_frame, yuvFrame);

    return yuvFrame;
  }
}
