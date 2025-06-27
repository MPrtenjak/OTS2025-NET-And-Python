using CSLibrary;
using CSWebApi.EndPoints;
using Scalar.AspNetCore;

var appSettings = AppSettings.GetSettingsBasedOnCommandLineArguments(args);

var builder = WebApplication.CreateBuilder(args);

// Register services, including the Python service
builder.Services.InitPython(appSettings);
builder.Services.AddSingleton<AppSettings>(appSettings);

// Add OpenAPI services
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
  app.MapScalarApiReference();
}

app.UseHttpsRedirection();

// Map API endpoints
HelloWorldEndpoints.MapEndpoints(app);
DragonEndpoints.MapEndpoints(app);
PdfFilesEndpoints.MapEndpoints(app);
GraphEndpoints.MapEndpoints(app);
WebReaderEndpoint.MapEndpoints(app);

app.Run();
