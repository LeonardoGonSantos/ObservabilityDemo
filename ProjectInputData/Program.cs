using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using OpenTelemetry.Extensions.Hosting;
using Examples.Console;

var builder = WebApplication.CreateBuilder(args);

var serviceName = "MyCompany.MyProduct.MyService";
var serviceVersion = "1.0.0"; 

builder.Services.AddOpenTelemetry()
  .WithTracing(b =>
  {
      b
      .AddOtlpExporter(a => a.Endpoint = new Uri("http://localhost:4315"))
      .AddSource(serviceName)
      .ConfigureResource(resource =>
          resource.AddService(
            serviceName: serviceName,
            serviceVersion: serviceVersion));
  });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/forceLogging", () =>
{
    TestOtlpExporter.Run("http://localhost:4315", "grpc");
    return "teste ok";
})
.WithName("Observability Teste")
.WithOpenApi();


app.Run();
