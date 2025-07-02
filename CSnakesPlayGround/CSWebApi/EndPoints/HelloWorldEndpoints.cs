using CSnakes.Runtime;
using CSLibrary.Examples;

namespace CSnakesWebApi.EndPoints;

public static class HelloWorldEndpoints
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/hello", (IPythonEnvironment pythonEnvironment, string userName) =>
    {
      var example = new HelloWorldExample(pythonEnvironment, userName ?? "WebApp");
      example.Run();

      return example.Result;
    });
  }
}