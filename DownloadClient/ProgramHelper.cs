using System.Diagnostics;
using System.Text.Json;

using DownloadClient.Contracts;
using DownloadClient.Extensions;
using DownloadClient.Services;

using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

internal class ProgramHelper(ILogger<ProgramHelper> logger, SmhiService smhiService, IObservationsService dbService)
{
  internal async Task InitDatabase()
  {
    await dbService.CreateDb();

    if (await dbService.IsEmpty())
    {
      logger.LogInformation("Database is empty");
      await AddLegacyData();
    }
    else
    {
      logger.LogInformation("Database is not empty");
    }
  }

  internal async Task AddLegacyData()
  {
    Stopwatch sw = Stopwatch.StartNew();
    string json = await File.ReadAllTextAsync("./total.json");
    IEnumerable<TotalRecord>? totalRecords = JsonSerializer.Deserialize<IEnumerable<TotalRecord>>(json);
    if (totalRecords is not null)
    {
      IEnumerable<Observation> obs = totalRecords.ToEntities();
      await dbService.AddObservations(obs, false);
    }
    sw.Stop();
    logger.LogInformation("Added archive: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
  }

  internal async Task<IEnumerable<Observation>> ParseSmhiData()
  {
    Stopwatch sw = Stopwatch.StartNew();
    IEnumerable<SmhiObservations> smhiObservations = await smhiService.GetObservations();
    sw.Stop();
    logger.LogInformation("Read observations: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);

    if (smhiObservations is null || !smhiObservations.Any())
    {
      return Array.Empty<Observation>();
    }

    sw.Restart();    
    IEnumerable<Observation> observations = smhiObservations.ToEntities();
    sw.Stop();
    logger.LogInformation("Observations to entities: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);

    return observations;
  }

  internal async Task AddObservations(IEnumerable<Observation> observations)
  {
    await dbService.AddObservations(observations, false);
  }
}