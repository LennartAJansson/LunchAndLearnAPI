// See https://aka.ms/new-console-template for more information
//refitter --cancellation-tokens --namespace ApiTest.Services --use-api-response --immutable-records --output ./ApiTest/Services/IControllerApiClient.cs https://localhost:7138/swagger/v1/swagger.json
//refitter --cancellation-tokens --namespace ApiTest.Services --use-api-response --immutable-records --output ./ApiTest/Services/IFastEndpointApiClient.cs https://localhost:7228/swagger/v1/swagger.json
//refitter --cancellation-tokens --namespace ApiTest.Services --use-api-response --immutable-records --output ./ApiTest/Services/IMinimalApiClient.cs https://localhost:7282/swagger/v1/swagger.json

using System.Diagnostics;

using ApiTest.Services;

using Refit;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddRefitClient<IControllerApiClient>()
  .ConfigureHttpClient(c =>
  {
    c.BaseAddress = new Uri("https://localhost:7138/");
  });

builder.Services.AddRefitClient<IFastEndpointApiClient>()
  .ConfigureHttpClient(c =>
  {
    c.BaseAddress = new Uri("https://localhost:7228/");
  });

builder.Services.AddRefitClient<IMinimalApiClient>()
  .ConfigureHttpClient(c =>
  {
    c.BaseAddress = new Uri("https://localhost:7282/");
  });

IHost host = builder.Build();

IServiceScope scope = host.Services.CreateScope();

IControllerApiClient controllerApiClient = scope.ServiceProvider.GetRequiredService<IControllerApiClient>();
IFastEndpointApiClient fastEndpointApiClient = scope.ServiceProvider.GetRequiredService<IFastEndpointApiClient>();
IMinimalApiClient minimalApiClient = scope.ServiceProvider.GetRequiredService<IMinimalApiClient>();
ILogger<Program> logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

Stopwatch sw = Stopwatch.StartNew();

var minimalObservations = await minimalApiClient.GetObservations();
logger.LogInformation("MinimalApiClient.GetObservations ({count} records): {ms} ms = {ticks} ticks", minimalObservations.Content.Count(), sw.ElapsedMilliseconds, sw.ElapsedTicks);

var fastEndpointObservations = await fastEndpointApiClient.GetObservations();
logger.LogInformation("FastEndpointApiClient.GetObservations ({count} records): {ms} ms = {ticks} ticks", fastEndpointObservations.Content.Count(), sw.ElapsedMilliseconds, sw.ElapsedTicks);

var controllerObservations = await controllerApiClient.GetObservations();
logger.LogInformation("ControllerApiClient.GetObservations ({count} records): {ms} ms = {ticks} ticks", controllerObservations.Content.Count(), sw.ElapsedMilliseconds, sw.ElapsedTicks);

host.WaitForShutdown();
