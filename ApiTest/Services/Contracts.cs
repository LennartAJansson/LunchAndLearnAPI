using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiTest.Services;

public sealed class DatesResponse : List<DateTimeOffset>;
public sealed class ObservationsResponse: List<ObservationResponse>;

public partial class ObservationResponse
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
