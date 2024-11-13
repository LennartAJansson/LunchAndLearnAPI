using MinimalApi.Endpoints;

using WeatherObservations.Data.Context;
using WeatherObservations.Data.Extensions;
using WeatherObservations.Telemetry.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets<ObservationsContext>();

builder.AddWeatherObservationsTelemetry()
  .AddObservationsData();

builder.Services.AddOutputCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
  _ = app.UseSwagger();
  _ = app.UseSwaggerUI(c =>
  {
    c.ConfigObject.AdditionalItems.Add("syntaxHighlight", false);
  });
}

app.UseHttpsRedirection();
app.UseOutputCache();
app.UseObservationEndpoints();
app.UseWeatherObservationsTelemetry();

app.Run();
