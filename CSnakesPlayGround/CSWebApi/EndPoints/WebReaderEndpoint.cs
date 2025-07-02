using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSnakesWebApi.EndPoints;

public static class WebReaderEndpoint
{
  public static void MapEndpoints(WebApplication app, AppSettings appSettings)
  {
    app.MapGet("/web", (IPythonEnvironment pythonEnvironment) =>
    {
      var example = new WebReaderExample(pythonEnvironment, appSettings.WebDriverLocation, "A");
      example.Run();

      return example.Result;
    });
  }
}