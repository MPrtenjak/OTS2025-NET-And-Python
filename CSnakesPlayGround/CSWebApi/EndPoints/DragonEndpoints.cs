using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSWebApi.EndPoints;

public static class DragonEndpoints
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/dragon-story", (IPythonEnvironment pythonEnvironment, AppSettings appSettings) =>
    {
      var example = new OpenAiExample(pythonEnvironment, appSettings.OpenAiApiKey);
      example.Run();

      return Results.Ok(example.Result);
    });
  }
}