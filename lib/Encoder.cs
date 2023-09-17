using System.Diagnostics;
using Godot;

namespace GodotFFmpegTest;

public unsafe partial class Encoder : Node
{
	private Process process;

	public void Initialize(string ffmpegPath, int width, int height, string output)
	{
		process = new Process();
		process.StartInfo.FileName = ffmpegPath;
		process.StartInfo.Arguments = $"-y -f image2pipe -r 60 -i pipe:0 -s {width}x{height} {output}";
		process.StartInfo.CreateNoWindow = true;
		process.StartInfo.RedirectStandardInput = true;
		process.StartInfo.UseShellExecute = false;
		process.Start();
	}

	public void Encode(Image p_image)
	{
		var bytes = p_image.SavePngToBuffer();
		process.StandardInput.BaseStream.Write(bytes, 0, bytes.Length);
		process.StandardInput.Flush();
	}

	public void Stop()
	{
		process.StandardInput.Close();
		process.WaitForExit();
	}
}
