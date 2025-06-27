using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSWebApi.EndPoints;

public static class WebReaderEndpoint
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/web", (IPythonEnvironment pythonEnvironment, AppSettings appSettings) =>
    {
      var example = new WebReaderExample(pythonEnvironment, appSettings.WebDriverLocation, "A");
      example.Run();

      return example.Result;
    });
  }
}