using CSnakes.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CSLibrary;

public static class InitPythonEnvironment
{
  public static IServiceCollection InitPython(this IServiceCollection services, AppSettings settings)
  {
    var home = Path.Join(settings.ExecutingAssemblyFolder, "PythonScripts");
    services
        .WithPython()
        .WithHome(home)
        .FromFolder(settings.PythonFolder, settings.PythonVersion)
        .WithVirtualEnvironment(settings.VirtualPythonFolder)
        .WithPipInstaller()
        ;

    return services;
  }
}
