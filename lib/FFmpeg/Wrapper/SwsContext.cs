using System;
using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class SwsContext
{
  public AutoGen.Abstractions.SwsContext* Pointer;

  public SwsContext(AutoGen.Abstractions.SwsContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  public int SwsScale(AVFrame p_source, AVFrame p_dest)
  {
    return ffmpeg.sws_scale(Pointer, p_source.Pointer->data, p_source.Pointer->linesize, 0, p_source.Height, p_dest.Pointer->data, p_dest.Pointer->linesize);
  }

  public static SwsContext GetContext(AVFrame p_source, AVFrame p_dest)
  {
    return new(ffmpeg.sws_getContext(p_source.Width, p_source.Height, p_source.Format, p_dest.Width, p_dest.Height, p_dest.Format, 0, null, null, null));
  }
}
