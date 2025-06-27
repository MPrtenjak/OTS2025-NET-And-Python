using CSnakes.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

internal class Program
{
  private static void Main(string[] args)
  {
    var pdfFiles = args
        .Where(arg => File.Exists(arg) &&
                      Path.GetExtension(arg).Equals(".pdf", StringComparison.OrdinalIgnoreCase));

    if (!pdfFiles.Any())
    {
      Console.WriteLine("No PDFs to convert.");
      return;
    }

    var exeFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
    var baseFolder = Path.GetFullPath(Path.Combine(exeFolder, @"..\..\..\"));

    var host = Host.CreateDefaultBuilder(args)
      .ConfigureServices(s => s
        .WithPython()
        .FromFolder(Path.Combine(baseFolder, @"..\Python312\install"), "3.12")
        .WithVirtualEnvironment(Path.Combine(baseFolder, @"..\virtual-env"))
        .WithPipInstaller())
      .Build();

    var env = host.Services.GetRequiredService<IPythonEnvironment>();
    var module = env.Extract();
    var texts = module.ExtractPdfTexts(pdfFiles.ToArray());

    foreach (var text in texts)
      Console.WriteLine($"------------\n{text}\n------------\n");
  }
}
