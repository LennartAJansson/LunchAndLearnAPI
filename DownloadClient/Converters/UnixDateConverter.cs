namespace DownloadClient.Converters;

using System.Text.Json;
using System.Text.Json.Serialization;

//Since SMHI is using Unix dates which is a long, we need to convert it to a DateTime

public class UnixDateConverter : JsonConverter<DateTime>
{
  //This will convert from a unix date to datetime when reading the json
  public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
      => DateTime.UnixEpoch.AddMilliseconds(reader.GetInt64());

  //This will convert to a unix date from datetime when writing the json
  public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
      => writer.WriteNumberValue(((DateTimeOffset)dateTimeValue).ToUnixTimeSeconds());
}