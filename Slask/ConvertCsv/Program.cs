// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text;
using System.Text.Json;

using CsvHelper;
using CsvHelper.Configuration;

IEnumerable<CsvRecord> temp;
IEnumerable<CsvRecord> fukt;
IEnumerable<CsvRecord> tryck;

CsvConfiguration config = new(new CultureInfo("sv-SE"))
{
  //HasHeaderRecord = false,
  Delimiter = ";",
  Encoding = Encoding.UTF8,
  HasHeaderRecord = true,
};

using (StreamReader reader = new("./Temperatur.csv"))
using (CsvReader csv = new(reader, config))
{
  temp = csv.GetRecords<CsvRecord>().ToList();
}

using (StreamReader reader = new("./Luftfuktighet.csv"))
using (CsvReader csv = new(reader, config))
{
  fukt = csv.GetRecords<CsvRecord>().ToList();
}

using (StreamReader reader = new("./Lufttryck.csv"))
using (CsvReader csv = new(reader, config))
{
  tryck = csv.GetRecords<CsvRecord>().ToList();
}

Dictionary<DateTime, TotalRecord> totalRecords = [];

foreach (CsvRecord item in temp)
{
  totalRecords.Add(item.Date.ToDateTime(item.Time), new TotalRecord
  {
    Date = item.Date.ToDateTime(item.Time),
    Temperatur = item.Value,
  });
}

foreach (CsvRecord item in fukt)
{
  if (totalRecords.TryGetValue(item.Date.ToDateTime(item.Time), out TotalRecord record))
  {
    record.Fuktighet = item.Value;
  }
  else
  {
    totalRecords.Add(item.Date.ToDateTime(item.Time), new TotalRecord
    {
      Date = item.Date.ToDateTime(item.Time),
      Fuktighet = item.Value,
    });
  }
}

foreach (CsvRecord item in tryck)
{
  if (totalRecords.TryGetValue(item.Date.ToDateTime(item.Time), out TotalRecord record))
  {
    record.Tryck = item.Value;
  }
  else
  {
    totalRecords.Add(item.Date.ToDateTime(item.Time), new TotalRecord
    {
      Date = item.Date.ToDateTime(item.Time),
      Tryck = item.Value,
    });
  }
}

string json = JsonSerializer.Serialize(totalRecords.Values, new JsonSerializerOptions 
  { 
    WriteIndented = true,
  });
File.WriteAllText("Total.json", json);
