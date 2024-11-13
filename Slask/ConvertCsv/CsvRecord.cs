// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;

public sealed class CsvRecord
{
  [JsonPropertyName("Date")]
  public DateOnly Date { get; set; }
  [JsonPropertyName("Time")]
  public TimeOnly Time { get; set; }
  [JsonPropertyName("Value")]
  public float Value { get; set; }
  [JsonPropertyName("Quality")]
  public char Quality { get; set; }
}
