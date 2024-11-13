namespace FastEndpointApi.GetObservations;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

internal sealed class ObservationsEndpoint(IObservationsService service)
  : EndpointWithoutRequest<ObservationsResponse, ObservationsMapper>
{
  public override void Configure()
  {
    Get("/observations");
    AllowAnonymous();
    Description(b => b
      .Produces<ObservationsResponse>(200, "application/json"),
      //.ProducesProblemFE(400)
      //.ProducesProblemFE<InternalErrorResponse>(500),
      clearDefaults: true);
    this.ResponseCache(3600);
  }

  public override async Task HandleAsync(CancellationToken c)
  {
    var response = await service.GetObservations();
    
    await SendAsync(Map.FromEntity(response), cancellation: c);
  }
}
