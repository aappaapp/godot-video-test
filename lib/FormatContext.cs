using FFmpeg.AutoGen.Abstractions;
using Godot;
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FFmpeg.Wrapper;

public unsafe class FormatContext : IDisposable
{
  public AVFormatContext* Pointer;

  public FormatContext()
  {
    int ret;

    fixed (AVFormatContext** ptr_Pointer = &Pointer)
      ret = ffmpeg.avformat_alloc_output_context2(ptr_Pointer, null, "mp4", null);
    if (ret < 0)
      throw new Exception(ret.ToString());
    Save();
  }

  public void Dispose()
  {
    ffmpeg.avformat_free_context(Pointer);
  }

  public void InterleavedWriteFrame(Packet p_packet)
  {
    int ret = ffmpeg.av_interleaved_write_frame(Pointer, p_packet.Pointer);
    if (ret < 0)
      throw new Exception(ret.ToString());
  }

  public Stream NewStream()
  {
    Stream stream = new(ffmpeg.avformat_new_stream(Pointer, null));
    stream.Pointer->codecpar->codec_id = AVCodecID.AV_CODEC_ID_HEVC;
    stream.Pointer->codecpar->codec_type = AVMediaType.AVMEDIA_TYPE_VIDEO;
    return stream;
  }

  public void Save()
  {
    NewStream();

    int ret = ffmpeg.avio_open(&Pointer->pb, "C:\\a\\output.mp4", ffmpeg.AVIO_FLAG_READ_WRITE);
    if (ret < 0)
      throw new Exception($"Could not open '': {ret}\n");

  }

  public void WriteHeader()
  {
    int ret = ffmpeg.avformat_write_header(Pointer, null);
    if (ret < 0)
      throw new Exception(ret.ToString());
  }

  public void WriteTrailer()
  {
    int ret = ffmpeg.av_write_trailer(Pointer);
    if (ret < 0)
      throw new Exception(ret.ToString());
  }
}
