using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSnakesWebApi.EndPoints;

public static class GraphEndpoints
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/graph", async (IPythonEnvironment pythonEnvironment) =>
    {
      try
      {
        var example = new GraphExample(pythonEnvironment);
        example.Run();

        var imageFilePath = Path.GetTempFileName() + ".png";
        example.SaveToFile(imageFilePath);

        return Results.File(imageFilePath, "image/png", "graph.png");
      }
      catch (Exception ex)
      {
        return Results.Problem($"An error occurred: {ex.Message}");
      }
    });
  }
}