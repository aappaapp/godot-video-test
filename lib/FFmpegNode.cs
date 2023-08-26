using Godot;
using FFmpeg.AutoGen;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;


namespace GodotFFmpegTest;

[GlobalClass]
public unsafe partial class FFmpegNode : Node
{
	private AVFormatContext* context;

	public void Initialize(string path)
	{
		ffmpeg.RootPath = path;
		context = ffmpeg.avformat_alloc_context();
	}

	public void Add(byte[] buffer)
	{
		var frame = ffmpeg.av_frame_alloc();
		var buf = ffmpeg.av_buffer_alloc((ulong)buffer.Length);
		Marshal.Copy(buffer, 0, new IntPtr(buf->data), buffer.Length);
	}

	public byte[] Combine()
	{
		ffmpeg.avformat_free_context(context);
		GD.Print(Marshal.PtrToStringUTF8(new IntPtr(context->video_codec->name)));
		return new byte[] { };
	}
}
