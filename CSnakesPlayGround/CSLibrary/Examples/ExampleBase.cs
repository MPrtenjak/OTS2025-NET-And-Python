using CSnakes.Runtime;

namespace CSLibrary.Examples;

public class ExampleBase<T>(string name, IPythonEnvironment pythonEnvironment)
{
  protected IPythonEnvironment _pythonEnvironment = pythonEnvironment;

  public string Name { get; private set; } = name;
  public T Result { get; protected set; } = default!;
  public virtual string ResultAsString
  {
    get => Result?.ToString() ?? string.Empty;
  }
}
