using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace CSLibrary;

public class AppSettings
{
  public string PythonFolder { get; set; } = null!;
  public string PythonVersion { get; set; } = null!;
  public string VirtualPythonFolder { get; set; } = null!;
  public string WebDriverLocation { get; set; } = null!;
  public string OpenAiApiKey { get; set; } = null!;
  public string ExecutingAssemblyFolder { get; set; } = null!;

  public static AppSettings LoadSettings(string key)
  {
    var executingAssemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
      ?? throw new InvalidOperationException("Unable to determine the executing assembly folder.");

    var configuration = new ConfigurationBuilder()
        .SetBasePath(executingAssemblyFolder)
        .AddJsonFile("appsettings.json")
        .Build();

    var settings = new AppSettings();
    configuration.GetSection(key).Bind(settings);
    settings.ExecutingAssemblyFolder = executingAssemblyFolder;

    if (string.IsNullOrEmpty(settings.PythonFolder))
      throw new ArgumentException($"PythonFolder is not set in the configuration for key '{key}'.");

    string? apiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
    if (string.IsNullOrEmpty(apiKey))
      throw new ArgumentException($"Please set the OPEN_AI_API_KEY environment variable.");

    settings.OpenAiApiKey = apiKey;
    return settings;
  }

  public static AppSettings GetSettingsBasedOnCommandLineArguments(string[] commandLineArgs)
  {
    string? computer = commandLineArgs.Length > 0 ? commandLineArgs[0] : null;
    if (string.IsNullOrEmpty(computer))
      throw new ArgumentException("Computer name is not provided. Please provide a computer name as an argument.");

    return LoadSettings(computer);
  }
}
