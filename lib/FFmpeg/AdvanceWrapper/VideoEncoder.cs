using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using FFmpeg.Wrapper;
using Godot;

namespace FFmpeg.AdvanceWrapper;

public class VideoEncoder
{
  public AVCodec Codec;
  public AVCodecContext CodecContext;
  public AVFormatContext Format;
  public AVPacket Packet;
  public AVStream Stream;

  public List<byte> bytes = new();


  public VideoEncoder(int p_width, int p_height)
  {
    Codec = AVCodec.FindEncoder(AutoGen.Abstractions.AVCodecID.AV_CODEC_ID_HEVC);
    CodecContext = AVCodecContext.AllocContext(Codec);

    CodecContext.SetFramerate(60);
    CodecContext.SetResolution(p_width, p_height);
    CodecContext.BitRate = 400000;
    CodecContext.PixelFormat = AutoGen.Abstractions.AVPixelFormat.AV_PIX_FMT_YUV420P;

    CodecContext.Open(Codec).ThrowIfLessThanZero();


    Packet = AVPacket.Alloc();


    Format = AVFormatContext.AllocContext();
    Format.AllocOutputContext("mp4").ThrowIfLessThanZero();
    Stream = Format.NewStream();
    Stream.Id = (int)Format.NumberStreams - 1;
    Format.Open("C:/a/output.mp4").ThrowIfLessThanZero();
    unsafe
    {
      if ((Format.Pointer->oformat->flags & AutoGen.ffmpeg.AVFMT_GLOBALHEADER) > 0)
      {
        CodecContext.Pointer->flags |= AutoGen.ffmpeg.AV_CODEC_FLAG_GLOBAL_HEADER;
      }
    }
    // AutoGen.Abstractions.ffmpeg.AVFMT_GLOBALHEADER }
    Format.WriteHeader().ThrowIfLessThanZero();
  }

  public void Encode(AVFrame frame)
  {
    CodecContext.SendFrame(frame).ThrowIfLessThanZero();
    int ret = 0;
    while (ret >= 0)
    {
      ret = CodecContext.RecievePacket(Packet);
      if (ret == AutoGen.Abstractions.ffmpeg.EAGAIN.AVERROR() || ret == AutoGen.Abstractions.ffmpeg.AVERROR_EOF)
        return;
      else
        ret.ThrowIfLessThanZero();

      Packet.StreamIndex = Stream.Id;

      Format.InterleavedWriteFrame(Packet).ThrowIfLessThanZero();
    }
  }

  public void Finish()
  {
  }
}
