namespace DownloadClient.Extensions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using DownloadClient.Contracts;

using WeatherObservations.Domain.Abstract.Model;

internal static class Transformers
{
  public static IEnumerable<Observation> ToEntities(this IEnumerable<SmhiObservations> observation)
  {
    Stopwatch sw = Stopwatch.StartNew();

    SmhiObservations? temp = observation.FirstOrDefault(o => o.Parameter?.Key == "1");
    SmhiObservations? hum = observation.FirstOrDefault(o => o.Parameter?.Key == "6");
    SmhiObservations? press = observation.FirstOrDefault(o => o.Parameter?.Key == "9");

    if (temp == null || hum == null || press == null)
    {
      yield break;
    }

    SmhiObservationValue[]? tempValues = temp.Values;
    SmhiObservationValue[]? pressValues = press.Values;
    SmhiObservationValue[]? humValues = hum.Values;

    SmhiObservationPosition[]? tempPositions = temp.Positions;
    if (tempValues == null || pressValues == null || humValues == null || tempPositions == null)
    {
      yield break;
    }

    foreach (SmhiObservationValue temperatureValue in tempValues)
    {
      SmhiObservationPosition? lastPosition = tempPositions.OrderBy(p => p.From).LastOrDefault();
      if (lastPosition == null)
      {
        continue;
      }

      SmhiObservationValue? pressureValue = pressValues.FirstOrDefault(p => p.Date == temperatureValue.Date);
      SmhiObservationValue? humidityValue = humValues.FirstOrDefault(p => p.Date == temperatureValue.Date);

      if (pressureValue == null || humidityValue == null)
      {
        continue;
      }

      yield return new Observation
      {
        StationKey = temp.Station!.Key!,
        StationName = temp.Station!.Name!,
        Longitude = lastPosition.Longitude,
        Latitude = lastPosition.Latitude,
        Updated = temp.Updated,
        MeasuredDate = temperatureValue.Date,
        Temperature = temperatureValue.Measured!.ToFloatFromDot(),//.Measured is string
        Pressure = pressureValue.Measured!.ToFloatFromDot(),//.Measured is string
        Humidity = humidityValue.Measured!.ToFloatFromDot(),//.Measured is string
      };
    }
  }

  public static IEnumerable<Observation> ToEntities(this IEnumerable<TotalRecord> records)
    => records.Select(r => new Observation
    {
      StationKey = "107420",
      StationName = "Gävle A",
      Longitude = 17.1607,
      Latitude = 60.7161,
      Updated = r.Date,
      MeasuredDate = r.Date,
      Temperature = r.Temperature,
      Pressure = r.Pressure,
      Humidity = r.Humidity,
    });

  public static float ToFloatFromDot(this string value)
  {
    return float.Parse(value, CultureInfo.GetCultureInfo("en-US"));
  }

  public static float ToFloatFromComma(this string value)
  {
    return float.Parse(value, CultureInfo.GetCultureInfo("sv-SE"));
  }
}
