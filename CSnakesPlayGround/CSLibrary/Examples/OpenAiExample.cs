using CSnakes.Runtime;

namespace CSLibrary.Examples;

public class OpenAiExample(IPythonEnvironment pythonEnvironment, string apiKey) :
  ExampleBase<string>("Open AI", pythonEnvironment),
  IExample<string>
{
  private readonly string _apiKey = apiKey;

  public void Run()
  {
    var module = _pythonEnvironment.OpenAi();
    Result = module.Dragon(_apiKey);
  }
}
