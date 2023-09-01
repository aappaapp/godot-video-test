using System.Runtime.InteropServices;
using FFmpeg.AutoGen.Abstractions;

namespace FFmpeg.Wrapper;

public unsafe class AVCodec
{
  public AutoGen.Abstractions.AVCodec* Pointer;

  public AVCodecID Id => Pointer->id;
  public string LongName => Marshal.PtrToStringUTF8(new(Pointer->long_name));
  public string Name => Marshal.PtrToStringUTF8(new(Pointer->name));
  public string WrapperName => Marshal.PtrToStringUTF8(new(Pointer->wrapper_name));

  public AVCodec(AutoGen.Abstractions.AVCodec* p_pointer)
  {
    Pointer = p_pointer;
  }

  public static AVCodec FindEncoder(AVCodecID id)
  {
    return new(ffmpeg.avcodec_find_encoder(id));
  }
}
