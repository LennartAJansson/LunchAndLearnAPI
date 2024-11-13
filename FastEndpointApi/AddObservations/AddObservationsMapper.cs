namespace FastEndpointApi.AddObservations;

using FastEndpoints;

using WeatherObservations.Domain.Abstract.Model;

internal sealed class AddObservationsMapper : Mapper<AddObservationsRequest, AddObservationsResponse, IEnumerable<Observation>>
{
  public override IEnumerable<Observation> ToEntity(AddObservationsRequest r) 
    => r.Observations.Select(o=>new Observation 
    {
      StationKey = "107420",
      StationName = "Gävle A",
      Latitude = 60.7161,
      Longitude = 17.1607,
      Updated = DateTimeOffset.Now,
      MeasuredDate = o.Date,
      Humidity = o.Humidity,
      Pressure = o.Pressure,
      Temperature = o.Temperature,
    });

  public override AddObservationsResponse FromEntity(IEnumerable<Observation> o)
  {
    var result = o.Select(r => new AddObservationsData
    {
      Date = r.MeasuredDate,
      Humidity = r.Humidity,
      Pressure = r.Pressure,
      Temperature = r.Temperature,
    });
    return new AddObservationsResponse { Observations = result };
  }
}