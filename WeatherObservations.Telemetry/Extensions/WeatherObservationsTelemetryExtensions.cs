namespace WeatherObservations.Telemetry.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;

public static class WeatherObservationsTelemetryExtensions
{
  public static IHostApplicationBuilder AddWeatherObservationsTelemetry(this IHostApplicationBuilder builder)
  {
    _ = builder.Services
      .AddOpenTelemetry()
      .WithMetrics(metrics =>
      {
        _ = metrics.AddMeter("Microsoft.Extensions.Hosting");
        _ = metrics.AddMeter("Microsoft.AspNetCore.Hosting");
        _ = metrics.AddMeter("Microsoft.AspNetCore.Server.Kestrel");
        _ = metrics.AddMeter("System.Net.Http");
        _ = metrics.AddPrometheusExporter();
        _ = metrics.AddOtlpExporter();
      });
    _ = builder.Logging.AddOpenTelemetry(options =>
    {
      _ = options.AddOtlpExporter();
    });

    return builder;
  }

  public static WebApplication UseWeatherObservationsTelemetry(this WebApplication app)
  {
    _ = app.UseOpenTelemetryPrometheusScrapingEndpoint("/metrics");
    
    return app;
  }
}
