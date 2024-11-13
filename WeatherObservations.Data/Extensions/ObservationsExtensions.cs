namespace WeatherObservations.Data.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WeatherObservations.Data.Context;
using WeatherObservations.Data.Services;
using WeatherObservations.Domain.Abstract.Services;

public static class ObservationsExtensions
{
  public static IHostApplicationBuilder AddObservationsData(this IHostApplicationBuilder builder)
  {
    _ = builder.Services.AddObservationsData(builder.Configuration);

    return builder;
  }

  public static IServiceCollection AddObservationsData(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddDbContext<ObservationsContext>(options =>
    {
      string? connectionString = configuration.GetConnectionString("SmhiDb")
        ?? throw new ArgumentNullException(nameof(connectionString), "Value is null!");

      _ = options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });

    _ = services.AddTransient<IObservationsService, ObservationsService>();

    return services;
  }
}
