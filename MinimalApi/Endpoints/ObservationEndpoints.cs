namespace MinimalApi.Endpoints;

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

public static class ObservationEndpoints
{
  public static IEndpointRouteBuilder UseObservationEndpoints(this IEndpointRouteBuilder builder)
  {
    RouteGroupBuilder group = builder.MapGroup("/observations").WithTags("Observations");

    _ = group.MapPost("/", async ([FromBody] IEnumerable<Observation> observations, [FromServices] ILogger<Program> logger, IObservationsService service) =>
    {
      Stopwatch sw = Stopwatch.StartNew();
      await service.AddObservations(observations, false);
      sw.Stop();
      logger.LogInformation("AddObservations: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
      return observations;
    })
    .WithName("AddObservations")
    .WithOpenApi();

    _ = group.MapGet("/", async ([FromServices] ILogger<Program> logger, IObservationsService service) =>
    {
      Stopwatch sw = Stopwatch.StartNew();
      IEnumerable<Observation> response = await service.GetObservations();
      sw.Stop();
      logger.LogInformation("GetObservations: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
      return response;
    })
    .WithName("GetObservations")
    .WithOpenApi()
    .CacheOutput();

    _ = group.MapGet("/{date}", async (DateTimeOffset date, [FromServices] ILogger<Program> logger, IObservationsService service) =>
    {
      Stopwatch sw = Stopwatch.StartNew();
      var response = await service.GetObservation(date);
      sw.Stop();
      logger.LogInformation("GetByDate: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
      return response;
    })
    .WithName("GetByDate")
    .WithOpenApi()
    .CacheOutput();

    _ = group.MapGet("/dates", async ([FromServices] ILogger<Program> logger, IObservationsService service) =>
    {
      Stopwatch sw = Stopwatch.StartNew();
      IEnumerable<DateTimeOffset> response = await service.GetMeasuredDates();
      sw.Stop();
      logger.LogInformation("GetMeasuredDates: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
      return response;
    })
    .WithName("GetMeasuredDates")
    .WithOpenApi()
    .CacheOutput();

    return builder;
  }
}
