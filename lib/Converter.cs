using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Godot;

namespace GodotFFmpegTest;

public unsafe partial class Converter : Node
{
  [Signal]
  public delegate void ConvertedEventHandler(string path);

  private bool done;
  private Process process;

  public void Initialize(string ffmpegPath, string inputFilePath)
  {
    process = new Process();
    process.StartInfo.FileName = ffmpegPath;
    process.StartInfo.Arguments = $"-y -i {inputFilePath} -f ogv C:/tmp/test.ogv -report";
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.UseShellExecute = false;
    process.Exited += (sender, e) => done = true;
    process.Start();
  }

  public override void _Process(double delta)
  {
    if (done)
    {
      done = false;
      EmitSignal(SignalName.Converted, "C:/tmp/test.ogv");
    }
  }
}
