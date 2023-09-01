using FFmpeg.AdvanceWrapper;
using FFmpeg.AutoGen.Bindings.DynamicallyLoaded;
using Godot;

namespace GodotFFmpegTest;

public unsafe partial class FFmpegNode : Node
{
	private VideoEncoder videoEncoder;

	public FFmpegNode()
	{
	}

	public void Initialize(string p_libraryPath)
	{
		DynamicallyLoadedBindings.LibrariesPath = p_libraryPath;
		DynamicallyLoadedBindings.Initialize();
		videoEncoder = new(1280, 720);
	}

	public void Encode(Image p_image)
	{
		videoEncoder.Encode(p_image.ToAVFrame());
	}
}
