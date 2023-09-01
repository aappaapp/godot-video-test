using FFmpeg.AdvanceWrapper;
using FFmpeg.Wrapper;
using Godot;

namespace GodotFFmpegTest;

public static class FFmpegUtils
{
  public unsafe static AVFrame ToAVFrame(this Image p_image)
  {
    AVFrame frame = AVFrame.Allocate();

    frame.Height = p_image.GetHeight();
    frame.Width = p_image.GetWidth();
    frame.Format = FFmpeg.AutoGen.Abstractions.AVPixelFormat.AV_PIX_FMT_RGB24;

    frame.GetBuffer();
    frame.MakeWritable();

    for (int y = 0; y < frame.Height; y++)
    {
      for (int x = 0; x < frame.Width; x++)
      {
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0]] = (byte)p_image.GetPixel(x, y).R8;
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0] + 1] = (byte)p_image.GetPixel(x, y).G8;
        frame.Pointer->data[0][x * 3 + y * frame.Pointer->linesize[0] + 2] = (byte)p_image.GetPixel(x, y).B8;
      }
    }


    return frame.ToYUV();
  }
}
