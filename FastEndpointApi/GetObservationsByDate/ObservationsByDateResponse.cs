namespace FastEndpointApi.GetObservationsByDate;
internal sealed class ObservationsByDateResponse
{
  public int Id { get; init; }

  public string? StationKey { get; init; }

  public string? StationName { get; init; }

  public double Longitude { get; init; }

  public double Latitude { get; init; }

  public DateTimeOffset Updated { get; init; }

  public DateTimeOffset MeasuredDate { get; init; }

  public float Temperature { get; init; }

  public float Pressure { get; init; }

  public float Humidity { get; init; }
}
