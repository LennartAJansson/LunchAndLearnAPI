namespace FastEndpointApi.GetObservations;

using FastEndpoints;

class ObservationsEndpointSummary
  : Summary<ObservationsEndpoint>
{
  public ObservationsEndpointSummary()
  {
    Summary = "short summary goes here";
    Description = "long description goes here";
    ResponseExamples[200] = new ObservationsResponse { };
    Responses[200] = "ok response description goes here";
    Responses[403] = "forbidden response description goes here";
  }
}