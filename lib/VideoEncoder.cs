using System;
using System.Reflection.Metadata.Ecma335;
using FFmpeg.AutoGen.Abstractions;
using FFmpeg.Wrapper;
using Godot;

namespace GodotFFmpegTest;

public unsafe class VideoEncoder : IDisposable
{
  public Encoder Encoder = null;

  public int width;
  public int height;

  public VideoEncoder(int p_width, int p_height)
  {
    height = p_height;
    width = p_width;

    Encoder = new Encoder(p_width, p_height, 30);
  }

  public void Encode(Image image)
  {
    int ret;
    Frame frame = new(image);
    Packet packet = new();

    ret = Encoder.SendFrame(frame);
    if (ret < 0)
      throw new Exception($"Error sending a frame for encoding: {ret}");

    while (ret >= 0)
    {
      ret = Encoder.RecievePacket(packet);
      if (ret == ffmpeg.AVERROR(ffmpeg.EAGAIN) || ret == ffmpeg.AVERROR_EOF)
        return;
      else if (ret < 0)
        throw new Exception("Error during encoding");
      GD.Print("Write!");
      packet.UnRef();
    }
  }

  public void Get()
  {
    // Encoder.RecievePacket
  }

  public void Dispose()
  {
    Encoder.Dispose();
  }
}
