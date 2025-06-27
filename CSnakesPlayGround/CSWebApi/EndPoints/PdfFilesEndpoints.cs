using CSnakes.Runtime;
using CSLibrary;
using CSLibrary.Examples;

namespace CSWebApi.EndPoints;

public static class PdfFilesEndpoints
{
  public static void MapEndpoints(WebApplication app)
  {
    app.MapGet("/scan-pdf", (IPythonEnvironment pythonEnvironment, AppSettings appSettings) =>
    {
      var pdfFileName = Path.Combine(appSettings.ExecutingAssemblyFolder, "TestFiles", "CSnakes.pdf");

      var example = new PDFExample(pythonEnvironment, pdfFileName);
      example.Run();

      return example.Result;
    });
  }
}