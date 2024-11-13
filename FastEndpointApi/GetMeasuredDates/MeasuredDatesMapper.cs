namespace FastEndpointApi.GetMeasuredDates;

using FastEndpoints;

internal sealed class MeasuredDatesMapper
  : ResponseMapper<MeasuredDatesResponse, IEnumerable<DateTimeOffset>>
{
  public override MeasuredDatesResponse FromEntity(IEnumerable<DateTimeOffset> e)
  {
    var response = new MeasuredDatesResponse();
    response.AddRange(e);
    return response;
  }
}