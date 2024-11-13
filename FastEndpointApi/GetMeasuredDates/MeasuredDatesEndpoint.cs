namespace FastEndpointApi.GetMeasuredDates;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Services;

internal sealed class MeasuredDatesEndpoint(IObservationsService service)
  : EndpointWithoutRequest<MeasuredDatesResponse, MeasuredDatesMapper>
{
  public override void Configure()
  {
    Get("/observations/dates");
    AllowAnonymous();
    Description(b => b
      .Produces<MeasuredDatesResponse>(200, "application/json"),
      //.ProducesProblemFE(400) //shortcut for .Produces<ErrorResponse>(400)
      //.ProducesProblemFE<InternalErrorResponse>(500),
      clearDefaults: true);
    this.ResponseCache(3600);
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    var response = await service.GetMeasuredDates();

    await SendAsync(Map.FromEntity(response), cancellation: c);
  }
}
