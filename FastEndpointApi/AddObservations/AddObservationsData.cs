namespace FastEndpointApi.AddObservations;

using WeatherObservations.Domain.Abstract.Model;

internal sealed class AddObservationsData
{
  public DateTimeOffset Date { get; set; }
  public float Temperature { get; set; }
  public float Humidity { get; set; }
  public float Pressure { get; set; }
}
