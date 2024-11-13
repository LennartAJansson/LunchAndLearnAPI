namespace FastEndpointApi.AddObservations;

using FastEndpoints;

internal sealed class AddObservationsRequest
{
  public required IEnumerable<AddObservationsData> Observations { get; set; }
  
  internal sealed class AddObservationsValidator : Validator<AddObservationsRequest>
  {
    public AddObservationsValidator()
    {

    }
  }
}
