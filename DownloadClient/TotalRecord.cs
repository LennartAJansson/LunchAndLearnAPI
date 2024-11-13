// See https://aka.ms/new-console-template for more information
public sealed class TotalRecord
{
  public DateTime Date { get; set; }
  public float Temperature { get; set; }
  public required string TemperatureQuality { get; set; }
  public float Humidity { get; set; }
  public required string HumidityQuality { get; set; }
  public float Pressure { get; set; }
  public required object PressureQuality { get; set; }
}

