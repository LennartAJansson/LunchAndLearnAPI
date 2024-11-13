// See https://aka.ms/new-console-template for more information
using DownloadClient.Extensions;

using WeatherObservations.Data.Context;
using WeatherObservations.Data.Extensions;
using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Telemetry.Extensions;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddUserSecrets<ObservationsContext>();
builder.Services.AddTransient<ProgramHelper>();

builder.AddWeatherObservationsTelemetry()
  .AddSmhiService()
  .AddObservationsData();

IHost app = builder.Build();

await app.StartAsync();

using IServiceScope scope = app.Services.CreateScope();

ProgramHelper helper = scope.ServiceProvider.GetRequiredService<ProgramHelper>();

await helper.InitDatabase();//Make sure db exists and is populated with historical data

IEnumerable<Observation> observations = await helper.ParseSmhiData();//Get latest months

await helper.AddObservations(observations);

await app.StopAsync();
