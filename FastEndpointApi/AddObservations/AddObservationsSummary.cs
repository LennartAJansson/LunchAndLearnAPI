namespace FastEndpointApi.AddObservations;
using FastEndpointApi.GetObservationsByDate;

using FastEndpoints;

internal sealed class AddObservationsSummary
  : Summary<AddObservationsEndpoint>
{
  public AddObservationsSummary()
  {
    Summary = "short summary goes here";
    Description = "long description goes here";
    //ExampleRequest = new AddObservationsRequest { };
    //ResponseExamples[200] = new AddObservationsResponse { };
    Responses[200] = "ok response description goes here";
    Responses[403] = "forbidden response description goes here";
  }
}