using FFmpeg.AutoGen.Abstractions;
using FFmpeg.AutoGen.Bindings.DynamicallyLoaded;
using Godot;
using System;
using System.Runtime.InteropServices;


namespace GodotFFmpegTest;

[GlobalClass]
public unsafe partial class FFmpegNode : Node
{
	private VideoEncoder videoEncoder;

	public FFmpegNode()
	{
	}

	public void Initialize(string path)
	{
		DynamicallyLoadedBindings.LibrariesPath = path;
		DynamicallyLoadedBindings.Initialize();
		videoEncoder = new VideoEncoder(1280, 720);
	}

	public void Add(Image image)
	{
		videoEncoder.SendImage(image);
	}

	public byte[] Get()
	{
		videoEncoder.Get();
		return new byte[] { };
	}
}
