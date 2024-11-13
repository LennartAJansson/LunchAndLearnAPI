using FastEndpointApi.GetMeasuredDates;

using FastEndpoints;
using FastEndpoints.Swagger;

using WeatherObservations.Data.Context;
using WeatherObservations.Data.Extensions;
using WeatherObservations.Telemetry.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<ObservationsContext>();

builder.AddWeatherObservationsTelemetry()
  .AddObservationsData();

builder.Services
  .AddFastEndpoints(o => o.Assemblies = new[]
  {
      typeof(MeasuredDatesEndpoint).Assembly,
  })
  .SwaggerDocument(o =>
  {
    o.DocumentSettings = s =>
    {
      s.Title = "FastEndpointsApi";
      s.Version = "v1";
    };
  })
  .AddResponseCaching();

WebApplication app = builder.Build();

app.UseResponseCaching()
  .UseFastEndpoints()
  .UseSwaggerGen();

app.UseWeatherObservationsTelemetry();

app.Run();
