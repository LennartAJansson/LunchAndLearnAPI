namespace FastEndpointApi.GetObservations;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Model;

internal sealed class ObservationsMapper
  : ResponseMapper<ObservationsResponse, IEnumerable<Observation>>
{
  public override ObservationsResponse FromEntity(IEnumerable<Observation> e)
  {
    var response = new ObservationsResponse();
    response.AddRange(e.Select(o => new ObservationResponse
    {
      Id = o.Id,
      StationKey = o.StationKey,
      StationName = o.StationName,
      Longitude = o.Longitude,
      Latitude = o.Latitude,
      Updated = o.Updated,
      MeasuredDate = o.MeasuredDate,
      Temperature = o.Temperature,
      Pressure = o.Pressure,
      Humidity = o.Humidity
    }));
    return response;
  }
}