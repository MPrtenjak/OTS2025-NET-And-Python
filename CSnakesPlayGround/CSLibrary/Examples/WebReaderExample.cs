using CSnakes.Runtime;
using System.Text;

namespace CSLibrary.Examples;

public class WebReaderExample(IPythonEnvironment pythonEnvironment, string webDriverLocation, string tableName) :
  ExampleBase<string>("Get data from web", pythonEnvironment),
  IExample<string>
{
  private readonly string _tableName = tableName ?? "A";
  private readonly string _webDriverLocation = webDriverLocation;

  public void Run()
  {
    var module = _pythonEnvironment.FetchTableFromWeb();
    var stocks = module.FetchByDataIdList(_tableName, _webDriverLocation);

    const string stockIdColumn = "Oznaka";
    const string stockValueColumn = "Zadnji";

    var sb = new StringBuilder();
    for (int s = 0; s < stocks.Count; s++)
    {
      sb.Append($"{stocks[s][stockIdColumn]}: {stocks[s][stockValueColumn],10}\n");
    }

    Result = sb.ToString();
  }
}