namespace WeatherObservations.Domain.Abstract.Model;

public class Observation
{
  public int Id { get; set; }
  public required string StationKey { get; set; }
  public required string StationName { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }
  public DateTimeOffset Updated { get; set; }
  public DateTimeOffset MeasuredDate { get; set; }
  public required float Temperature { get; set; }
  public required float Pressure { get; set; }
  public required float Humidity { get; set; }
}