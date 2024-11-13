namespace FastEndpointApi.AddObservations;
using FastEndpointApi.GetMeasuredDates;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Services;

internal sealed class AddObservationsEndpoint(IObservationsService service)
  : Endpoint<AddObservationsRequest, AddObservationsResponse, AddObservationsMapper>
{
  public override void Configure()
  {
    Post("/observations");
    AllowAnonymous();
    Description(b => b
      .Accepts<AddObservationsRequest>("application/json")
      .Produces<AddObservationsResponse>(200, "application/json"),
      //.ProducesProblemFE(400) //shortcut for .Produces<ErrorResponse>(400)
      //.ProducesProblemFE<InternalErrorResponse>(500),
      clearDefaults: true);
  }

  public override async Task HandleAsync(AddObservationsRequest r, CancellationToken c)
  {
    var observations = Map.ToEntity(r);
    await service.AddObservations(observations, false);
    var response = Map.FromEntity(observations);
    await SendAsync(response);
  }
}
