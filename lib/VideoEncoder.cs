using System;
using System.Collections.Generic;
using FFmpeg.AutoGen.Abstractions;
using FFmpeg.Wrapper;
using Godot;

namespace GodotFFmpegTest;

public unsafe class VideoEncoder : IDisposable
{
  public Encoder encoder = null;
  public FormatContext format = null;
  public List<byte> data = new();

  public Frame frame;
  public Packet packet;

  public int width;
  public int height;

  private int pts = 0;

  public VideoEncoder(int p_width, int p_height)
  {
    height = p_height;
    width = p_width;

    encoder = new(p_width, p_height, 30);
    format = new();
    packet = new();
    packet.Pointer->stream_index = 0;
    // Stream stream = format.NewStream();
    // stream.Pointer->id;
  }

  public void Encode(Image image)
  {
    int ret;
    frame = Frame.FromGodotImage(image);

    frame.Pointer->pts = pts++;

    ret = encoder.SendFrame(frame);
    if (ret < 0)
      throw new Exception($"Error sending a frame for encoding: {ret}");

    while (ret >= 0)
    {
      ret = encoder.RecievePacket(packet);

      if (ret == ffmpeg.AVERROR(ffmpeg.EAGAIN) || ret == ffmpeg.AVERROR_EOF)
        return;
      else if (ret < 0)
        throw new Exception("Error during encoding");


      format.InterleavedWriteFrame(packet);
    }
  }

  public byte[] Get()
  {
    return data.ToArray();
  }

  public void Dispose()
  {
    encoder.Dispose();
  }
}
