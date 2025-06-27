using System.Diagnostics;

namespace CSCmdLine;

public record Measurement(string Name, string Result, long ElapsedMilliseconds);

public class AppTimer
{
  private readonly Stopwatch _stopwatch = new();

  public AppTimer()
  {
    _stopwatch.Start();
  }

  public void Restart()
  {
    _stopwatch.Restart();
  }

  public Measurement ElapsedMilliseconds(string measurementName, string result)
  {
    _stopwatch.Stop();
    var elapsed = _stopwatch.ElapsedMilliseconds;
    _stopwatch.Restart();

    return new Measurement(measurementName, result, elapsed);
  }
}
