namespace DownloadClient.Contracts;

using System.Text.Json.Serialization;

using DownloadClient.Converters;

public class SmhiObservations
{
  [JsonPropertyName("value")]
  public SmhiObservationValue[]? Values { get; set; }

  [JsonPropertyName("updated")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime Updated { get; set; }

  [JsonPropertyName("parameter")]
  public SmhiObservationParameter? Parameter { get; set; }

  [JsonPropertyName("station")]
  public SmhiObservationStation? Station { get; set; }

  [JsonPropertyName("period")]
  public SmhiObservationPeriod? Period { get; set; }

  [JsonPropertyName("position")]
  public SmhiObservationPosition[]? Positions { get; set; }

  [JsonPropertyName("link")]
  public SmhiObservationLink[]? Links { get; set; }
}

public class SmhiObservationParameter
{
  [JsonPropertyName("key")]
  public string? Key { get; set; }

  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("summary")]
  public string? Summary { get; set; }

  [JsonPropertyName("unit")]
  public string? Unit { get; set; }
}

public class SmhiObservationStation
{
  [JsonPropertyName("key")]
  public string? Key { get; set; }

  [JsonPropertyName("name")]
  public string? Name { get; set; }

  [JsonPropertyName("owner")]
  public string? Owner { get; set; }

  [JsonPropertyName("ownerCategory")]
  public string? OwnerCategory { get; set; }

  [JsonPropertyName("measuringStations")]
  public string? MeasuringStations { get; set; }

  [JsonPropertyName("height")]
  public float Height { get; set; }
}

public class SmhiObservationPeriod
{
  [JsonPropertyName("key")]
  public string? Key { get; set; }

  [JsonPropertyName("from")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime From { get; set; }

  [JsonPropertyName("to")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime To { get; set; }

  [JsonPropertyName("summary")]
  public string? Summary { get; set; }

  [JsonPropertyName("sampling")]
  public string? Sampling { get; set; }
}

public class SmhiObservationValue
{
  [JsonPropertyName("date")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime Date { get; set; }

  [JsonPropertyName("value")]
  public string? Measured { get; set; }

  [JsonPropertyName("quality")]
  public string? Quality { get; set; }
}

public class SmhiObservationPosition
{
  [JsonPropertyName("from")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime From { get; set; }

  [JsonPropertyName("to")]
  [JsonConverter(typeof(UnixDateConverter))]
  public DateTime To { get; set; }

  [JsonPropertyName("height")]
  public float Height { get; set; }

  [JsonPropertyName("latitude")]
  public float Latitude { get; set; }

  [JsonPropertyName("longitude")]
  public float Longitude { get; set; }
}

public class SmhiObservationLink
{
  [JsonPropertyName("rel")]
  public string? Rel { get; set; }

  [JsonPropertyName("type")]
  public string? Type { get; set; }

  [JsonPropertyName("href")]
  public string? Href { get; set; }
}

public class SmhiObservationPeriodsForStation
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? owner { get; set; }
  public string? ownerCategory { get; set; }
  public string? measuringStations { get; set; }
  public bool active { get; set; }
  public string? summary { get; set; }
  public long from { get; set; }
  public long to { get; set; }
  public SmhiObservationPosition[]? position { get; set; }
  public SmhiObservationLink[]? link { get; set; }
  public SmhiObservationPeriodByStation[]? period { get; set; }
}

public class SmhiObservationPeriodByStation
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
}

public class SmhiObservationStationsForParameter
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public string? valueType { get; set; }
  public SmhiObservationLink[]? link { get; set; }
  public SmhiObservationStationsetByParameter[]? stationSet { get; set; }
  public SmhiObservationStationByParameter[]? station { get; set; }
}

public class SmhiObservationStationsetByParameter
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
}

public class SmhiObservationStationByParameter
{
  public string? name { get; set; }
  public string? owner { get; set; }
  public string? ownerCategory { get; set; }
  public string? measuringStations { get; set; }
  public int id { get; set; }
  public float height { get; set; }
  public float latitude { get; set; }
  public float longitude { get; set; }
  public bool active { get; set; }
  public long from { get; set; }
  public long to { get; set; }
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
}

public class SmhiObservationParametersForVersion
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
  public SmhiObservationResource[]? resource { get; set; }
}

public class SmhiObservationResource
{
  public SmhiObservationGeobox? geoBox { get; set; }
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
}

public class SmhiObservationGeobox
{
  public float minLatitude { get; set; }
  public float minLongitude { get; set; }
  public float maxLatitude { get; set; }
  public float maxLongitude { get; set; }
}

public class SmhiObservationVersionsForApi
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
  public SmhiObservationVersion[]? version { get; set; }
}

public class SmhiObservationVersion
{
  public string? key { get; set; }
  public long updated { get; set; }
  public string? title { get; set; }
  public string? summary { get; set; }
  public SmhiObservationLink[]? link { get; set; }
}