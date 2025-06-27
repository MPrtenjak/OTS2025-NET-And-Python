using CSLibrary;
using CSLibrary.Examples;
using CSCmdLine;
using CSnakes.Runtime;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var measurements = new List<Measurement>();
var appSettings = AppSettings.GetSettingsBasedOnCommandLineArguments(args);
var appTimer = new AppTimer();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => services.InitPython(appSettings));
measurements.Add(appTimer.ElapsedMilliseconds("Builder create", ""));

var app = builder.Build();
measurements.Add(appTimer.ElapsedMilliseconds("Builder build", ""));

var env = app.Services.GetRequiredService<IPythonEnvironment>();
measurements.Add(appTimer.ElapsedMilliseconds("Environment create", ""));

var pdfFileName = Path.Combine(appSettings.ExecutingAssemblyFolder, "TestFiles", "CSnakes.pdf");
List<IExample> examples = [
  new HelloWorldExample(env, "CommandLine"),
  new PDFExample(env, Path.Combine(appSettings.ExecutingAssemblyFolder, "TestFiles", "CSnakes.pdf")),
  new GraphExample(env),
  new OpenAiExample(env, appSettings.OpenAiApiKey),
  new WebReaderExample(env, appSettings.WebDriverLocation, "A"),
];

foreach (var example in examples)
{
  appTimer.Restart();
  example.Run();
  measurements.Add(appTimer.ElapsedMilliseconds(example.Name, example.ResultAsString));

  Console.WriteLine($"\n\n============================== Example: {example.Name}");
  Console.WriteLine(example.ResultAsString);

  if (example is GraphExample gExample)
    gExample.Show($"{Path.GetTempFileName()}.png");
}

Console.WriteLine($"\n\n==============================");
Console.WriteLine($"RESULTS:");
long totalTime = 0;
foreach (var item in measurements)
{
  Console.WriteLine($"{item.Name,-20}: {item.ElapsedMilliseconds,5} ms");
  totalTime += item.ElapsedMilliseconds;
}
Console.WriteLine("==============================");
Console.WriteLine($"{"Total time",-20}: {totalTime,5} ms");
