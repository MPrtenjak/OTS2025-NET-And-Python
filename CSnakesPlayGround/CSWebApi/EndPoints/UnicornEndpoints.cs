using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSnakesWebApi.EndPoints;

public static class UnicornEndpoints
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/unicorn-story", (IPythonEnvironment pythonEnvironment, AppSettings appSettings) =>
    {
      try
      {
        var example = new OpenAiExample(pythonEnvironment, appSettings.OpenAiApiKey);
        example.Run();

        return Results.Ok(example.Result);
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred: {ex.Message}");
      }
    });
  }
}