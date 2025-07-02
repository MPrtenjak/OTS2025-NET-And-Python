using CSnakes.Runtime;

namespace CSLibrary.Examples;

public class PDFExample(IPythonEnvironment pythonEnvironment, string pdfFileName) :
  ExampleBase<string>("Read PDF", pythonEnvironment),
  IExample<string>
{
  public void Run()
  {
    if (string.IsNullOrEmpty(pdfFileName))
      throw new ArgumentException("PDF file name cannot be null or empty.", nameof(pdfFileName));

    var module = _pythonEnvironment.PdfReader();
    Result = module.Read(pdfFileName);
  }
}