using CSnakes.Runtime;
using System.Diagnostics;

namespace CSLibrary.Examples;

public class GraphExample(IPythonEnvironment pythonEnvironment) :
  ExampleBase<byte[]>("Create graph", pythonEnvironment),
  IExample<byte[]>
{
  public void Run()
  {
    var module = _pythonEnvironment.ShowGraph();
    var graph = module.Graph();

    Result = graph.AsReadOnlySpan<byte>().ToArray();
  }

  public override string ResultAsString => "Graph Created";

  public string SaveToFile(string fileName)
  {
    if (string.IsNullOrEmpty(fileName))
      throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

    File.WriteAllBytes(fileName, Result);
    return fileName;
  }

  public void Show(string fileName)
  {
    SaveToFile(fileName);
    Process.Start("mspaint.exe", fileName);
  }
}
