namespace FastEndpointApi.GetObservationsByDate;

using FastEndpoints;

class ObservationsByDateEndpointSummary
  : Summary<ObservationsByDateEndpoint>
{
  public ObservationsByDateEndpointSummary()
  {
    Summary = "short summary goes here";
    Description = "long description goes here";
    ExampleRequest = new ObservationsByDateRequest { };
    ResponseExamples[200] = new ObservationsByDateResponse { };
    Responses[200] = "ok response description goes here";
    Responses[403] = "forbidden response description goes here";
  }
}