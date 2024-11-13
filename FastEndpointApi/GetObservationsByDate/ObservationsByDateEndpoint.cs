namespace FastEndpointApi.GetObservationsByDate;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

internal sealed class ObservationsByDateEndpoint(IObservationsService service)
  : Endpoint<ObservationsByDateRequest, ObservationsByDateResponse, ObservationsByDateMapper>
{
  public override void Configure()
  {
    Get("/observations/{date}");
    AllowAnonymous();
    Description(b => b
      //.Accepts<ObservationsByDateRequest>("application/json")
      .Produces<ObservationsByDateResponse>(200, "application/json"),
      //.ProducesProblemFE(400) //shortcut for .Produces<ErrorResponse>(400)
      //.ProducesProblemFE<InternalErrorResponse>(500),
      clearDefaults: true);
    this.ResponseCache(3600);
  }

  public override async Task HandleAsync(ObservationsByDateRequest r, CancellationToken c)
  {
    Observation? result = await service.GetObservation(r.Date);
    ObservationsByDateResponse? response = Map.FromEntity(result);
    if (response is null)
    {
      await SendNotFoundAsync();
    }
    else
    {
      await SendAsync(response, cancellation: c);
    }
  }
}
