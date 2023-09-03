using FFmpeg.AutoGen.Abstractions;
using Godot;

namespace FFmpeg.Wrapper;

public unsafe class AVFormatContext
{
  public AutoGen.Abstractions.AVFormatContext* Pointer;

  public uint NumberStreams => Pointer->nb_streams;

  public AVFormatContext(AutoGen.Abstractions.AVFormatContext* p_pointer)
  {
    Pointer = p_pointer;
  }

  ~AVFormatContext()
  {
    ffmpeg.avformat_free_context(Pointer);
  }

  public int AllocOutputContext(string p_formatName, string p_filename = null)
  {
    fixed (AutoGen.Abstractions.AVFormatContext** ptr_Pointer = &Pointer)
    {
      return ffmpeg.avformat_alloc_output_context2(ptr_Pointer, null, p_formatName, p_filename);
    }
  }

  public int InterleavedWriteFrame(AVPacket p_packet)
  {
    return ffmpeg.av_interleaved_write_frame(Pointer, p_packet.Pointer);
  }

  public AVStream NewStream()
  {
    return new(ffmpeg.avformat_new_stream(Pointer, null));
  }

  public int Open(string url)
  {
    return ffmpeg.avio_open(&Pointer->pb, url, ffmpeg.AVIO_FLAG_WRITE);
  }

  public int WriteHeader()
  {
    return ffmpeg.avformat_write_header(Pointer, null);
  }

  public static AVFormatContext AllocContext()
  {
    return new(ffmpeg.avformat_alloc_context());
  }

}
