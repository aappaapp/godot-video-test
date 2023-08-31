namespace FFmpeg.Bindings;

public unsafe class AVFrame
{
  public AutoGen.AVFrame* Pointer;

  public int Height => Pointer->height;
  public long PresentationTimestamp => Pointer->pts;
  public int Width => Pointer->width;

  public AVFrame(AutoGen.AVFrame* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVFrame()
  {
    fixed (AutoGen.AVFrame** ptr_Pointer = &Pointer)
      AutoGen.ffmpeg.av_frame_free(ptr_Pointer);
  }

  public static AVFrame Alloc()
  {
    return new(AutoGen.ffmpeg.av_frame_alloc());
  }
}
