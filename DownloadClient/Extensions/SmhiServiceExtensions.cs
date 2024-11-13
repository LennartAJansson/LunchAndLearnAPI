namespace DownloadClient.Extensions;

using DownloadClient.Clients;
using DownloadClient.Services;

using Refit;

public static class SmhiServiceExtensions
{
  public static IHostApplicationBuilder AddSmhiService(this IHostApplicationBuilder builder)
  {
    _ = builder.Services.AddSmhiService(builder.Configuration);

    return builder;
  }

  public static IServiceCollection AddSmhiService(this IServiceCollection services, IConfiguration configuration)
  {
    _ = services.AddRefitClient<ISmhiObservationApiClient>()
      .ConfigureHttpClient(c =>
      {
        c.BaseAddress = new Uri(configuration["BaseUrls:SmhiObservation"]!);
      });
    _ = services.AddTransient<SmhiService>();

    return services;
  }
}