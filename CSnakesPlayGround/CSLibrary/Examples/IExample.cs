namespace CSLibrary.Examples;

public interface IExample
{
  string Name { get; }
  string ResultAsString { get; }
  void Run();
}

public interface IExample<T> : IExample
{
  T Result { get; }
}