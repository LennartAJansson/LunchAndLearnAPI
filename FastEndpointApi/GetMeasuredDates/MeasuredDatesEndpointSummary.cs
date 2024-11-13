namespace FastEndpointApi.GetMeasuredDates;

using FastEndpoints;

class MeasuredDatesEndpointSummary
  : Summary<MeasuredDatesEndpoint>
{
  public MeasuredDatesEndpointSummary()
  {
    Summary = "short summary goes here";
    Description = "long description goes here";
    ResponseExamples[200] = new MeasuredDatesResponse { };
    Responses[200] = "ok response description goes here";
    Responses[403] = "forbidden response description goes here";
  }
}
