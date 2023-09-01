using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVCodecContext
{
  public AutoGen.Abstractions.AVCodecContext* Pointer;

  public long BitRate { get => Pointer->bit_rate; set => Pointer->bit_rate = value; }
  public int GroupOfPicturesSize { get => Pointer->gop_size; set => Pointer->gop_size = value; }
  public int Height { get => Pointer->height; set => Pointer->height = value; }
  public int MaxBFrames { get => Pointer->max_b_frames; set => Pointer->max_b_frames = value; }
  public AVPixelFormat PixelFormat { get => Pointer->pix_fmt; set => Pointer->pix_fmt = value; }
  public int Width { get => Pointer->width; set => Pointer->width = value; }

  public AVCodecContext(AutoGen.Abstractions.AVCodecContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVCodecContext()
  {
    fixed (AutoGen.Abstractions.AVCodecContext** ptr_Pointer = &Pointer)
      ffmpeg.avcodec_free_context(ptr_Pointer);
  }

  public int Open(AVCodec p_codec)
  {
    return ffmpeg.avcodec_open2(Pointer, p_codec.Pointer, null);
  }

  public int RecievePacket(AVPacket p_packet)
  {
    return ffmpeg.avcodec_receive_packet(Pointer, p_packet.Pointer);
  }

  public int SendFrame(AVFrame p_frame)
  {
    return ffmpeg.avcodec_send_frame(Pointer, p_frame.Pointer);
  }

  public static AVCodecContext AllocContext(AVCodec p_codec)
  {
    return new(ffmpeg.avcodec_alloc_context3(p_codec.Pointer));
  }
}
