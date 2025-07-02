using CSnakes.Runtime;

namespace CSLibrary.Examples;

public class HelloWorldExample(IPythonEnvironment pythonEnvironment, string userName) :
  ExampleBase<string>("Hello World", pythonEnvironment),
  IExample<string>
{
  public void Run()
  {
    var module = _pythonEnvironment.HelloWorld();
    Result = module.HelloWorld(userName);
  }
}
