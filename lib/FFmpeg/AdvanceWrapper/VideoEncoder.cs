using System;
using System.IO;
using System.Runtime.InteropServices;
using FFmpeg.Wrapper;
using Godot;

namespace FFmpeg.AdvanceWrapper;

public class VideoEncoder
{
  public AVCodec Codec;
  public AVCodecContext CodecContext;
  public AVPacket Packet;


  public VideoEncoder(int p_width, int p_height)
  {
    Codec = AVCodec.FindEncoder(AutoGen.Abstractions.AVCodecID.AV_CODEC_ID_HEVC);
    Packet = AVPacket.Alloc();
    CodecContext = AVCodecContext.AllocContext(Codec);
    CodecContext.SetFramerate(60);
    CodecContext.SetResolution(p_width, p_height);
    CodecContext.BitRate = 400000;
    CodecContext.PixelFormat = AutoGen.Abstractions.AVPixelFormat.AV_PIX_FMT_YUV420P;
    CodecContext.Open(Codec).ThrowIfLessThanZero();
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
    }
  }
}
