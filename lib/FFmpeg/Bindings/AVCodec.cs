using Godot;

namespace FFmpeg.Bindings;

public unsafe class AVCodec
{
  public AutoGen.AVCodec* Pointer;

  public AVCodec(AutoGen.AVCodec* p_pointer)
  {
    Pointer = p_pointer;
  }

  public static AVCodec FindEncoder(AutoGen.AVCodecID id)
  {
    return new(AutoGen.ffmpeg.avcodec_find_encoder(id));
  }
}
