using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Extensions.Hosting;
using Examples.Console;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//TestOtlpExporter.Run("http://127.0.0.1:4318", "http/protobuf");

TestOtlpExporter.Run("http://127.0.0.1:4315", "grpc");

app.Run();
