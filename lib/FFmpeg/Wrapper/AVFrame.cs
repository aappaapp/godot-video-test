using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVFrame
{
  public AutoGen.Abstractions.AVFrame* Pointer;

  public AVPixelFormat Format { get => (AVPixelFormat)Pointer->format; set => Pointer->format = (int)value; }
  public int Height { get => Pointer->height; set => Pointer->height = value; }
  public int Width { get => Pointer->width; set => Pointer->width = value; }

  public AVFrame(AutoGen.Abstractions.AVFrame* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVFrame()
  {
    fixed (AutoGen.Abstractions.AVFrame** ptr_Pointer = &Pointer)
    {
      ffmpeg.av_frame_free(ptr_Pointer);
    }
  }

  public int GetBuffer(int p_align = 0)
  {
    return ffmpeg.av_frame_get_buffer(Pointer, p_align);
  }

  public int MakeWritable()
  {
    return ffmpeg.av_frame_make_writable(Pointer);
  }

  public static AVFrame Allocate()
  {
    return new(ffmpeg.av_frame_alloc());
  }
}
