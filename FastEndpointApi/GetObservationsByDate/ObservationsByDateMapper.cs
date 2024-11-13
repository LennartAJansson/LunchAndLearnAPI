namespace FastEndpointApi.GetObservationsByDate;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Model;

internal sealed class ObservationsByDateMapper
  : Mapper<ObservationsByDateRequest, ObservationsByDateResponse, Observation>
{
  public override Observation ToEntity(ObservationsByDateRequest r)
    => base.ToEntity(r);

  public override ObservationsByDateResponse FromEntity(Observation o)
    => new()
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
    };
}