namespace WeatherObservations.Data.Services;

using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WeatherObservations.Data.Context;
using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

public class ObservationsService(ILogger<ObservationsService> logger,
  ObservationsContext context)
  : IObservationsService
{

  public Task<bool> IsEmpty()
    => Task.FromResult(!context.Observations.Any());

  public async Task<IEnumerable<DateTimeOffset>> GetMeasuredDates()
    => await context.Observations
      .Select(o => o.MeasuredDate)
      .ToListAsync();

  public async Task AddObservation(Observation observation, bool updateExisting)
  {
    Stopwatch sw = Stopwatch.StartNew();

    IEnumerable<DateTimeOffset> existingDates = await GetMeasuredDates();

    if (!existingDates.Contains(observation.MeasuredDate))
    {
      _ = await context.Observations.AddAsync(observation);
    }
    else if (updateExisting && observation.Id != 0)
    {
      _ = context.Observations.Update(observation);
    }

    _ = await context.SaveChangesAsync();

    sw.Stop();
    logger.LogInformation("AddObservation 1 record): {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
  }

  public async Task AddObservations(IEnumerable<Observation> observations, bool updateExisting)
  {
    Stopwatch sw = Stopwatch.StartNew();

    IEnumerable<DateTimeOffset> existingDates = await GetMeasuredDates();

    IEnumerable<Observation> newObservations = observations
      .Where(o => !existingDates
        .Contains(o.MeasuredDate));

    IEnumerable<Observation> existingObservations = observations
      .Where(o => existingDates
        .Contains(o.MeasuredDate));

    await context.Observations.AddRangeAsync(newObservations);

    if (updateExisting)
    {
      context.Observations.UpdateRange(existingObservations);
    }

    _ = await context.SaveChangesAsync();

    sw.Stop();
    logger.LogInformation("AddObservations ({cnt} records): {ms} ms = {ticks} ticks", observations.Count(), sw.ElapsedMilliseconds, sw.ElapsedTicks);
  }

  public async Task AddObservationsSlow(IEnumerable<Observation> observations, bool updateExisting)
  {
    Stopwatch sw = Stopwatch.StartNew();

    await context.Observations
      .AddRangeAsync(observations
        .Where(o => !context.Observations
          .Any(e => e.MeasuredDate == o.MeasuredDate)));

    if (updateExisting)
    {
      //Show me a way to update existing observations except the Id,
      //we only know the MeauredDate of each observation

      await UpdateObservations(observations
        .Where(o => context.Observations
          .Any(e => e.MeasuredDate == o.MeasuredDate)));
    }

    _ = await context.SaveChangesAsync();

    sw.Stop();
    logger.LogInformation("AddObservationsSlow ({cnt} records): {ms} ms = {ticks} ticks", observations.Count(), sw.ElapsedMilliseconds, sw.ElapsedTicks);
  }

  public async Task UpdateObservation(Observation observation)
  {
    Observation? existingObservation = await context.Observations.FirstOrDefaultAsync(o => o.MeasuredDate == observation.MeasuredDate);
    if (existingObservation is not null)
    {
      existingObservation.StationKey = observation.StationKey;
      existingObservation.StationName = observation.StationName;
      existingObservation.Longitude = observation.Longitude;
      existingObservation.Latitude = observation.Latitude;
      existingObservation.Updated = observation.Updated;
      existingObservation.Temperature = observation.Temperature;
      existingObservation.Pressure = observation.Pressure;
      existingObservation.Humidity = observation.Humidity;
    }
  }

  public async Task UpdateObservations(IEnumerable<Observation> observations)
  {
    foreach (Observation observation in observations)
    {
      await UpdateObservation(observation);
    }

    _ = await context.SaveChangesAsync();
  }

  public async Task CreateDb()
  {
    if (context.Database.GetPendingMigrations().Any())
    {
      await context.Database.MigrateAsync();
    }
  }

  public async Task ClearTable()
    => await context.Observations.ExecuteDeleteAsync();

  public async Task<Observation?> GetObservation(int id)
  {
    Stopwatch sw = Stopwatch.StartNew();
    Observation? observation = await context.Observations
      .AsNoTracking()
      .FirstOrDefaultAsync(i => i.Id == id);
    sw.Stop();
    logger.LogInformation("GetObservation: Id={id} {ms} ms = {ticks} ticks", id, sw.ElapsedMilliseconds, sw.ElapsedTicks);
    return observation;
  }

  public async Task<Observation?> GetObservation(DateTimeOffset date)
  {
    Stopwatch sw = Stopwatch.StartNew();
    Observation? observation = await context.Observations
      .AsNoTracking()
      .FirstOrDefaultAsync(i => i.MeasuredDate == date);
    sw.Stop();
    logger.LogInformation("GetObservation: Date={date} {ms} ms = {ticks} ticks", date, sw.ElapsedMilliseconds, sw.ElapsedTicks);
    return observation;
  }

  public async Task<IEnumerable<Observation>> GetObservations()
  {
    Stopwatch sw = Stopwatch.StartNew();
    IEnumerable<Observation> observations = await context.Observations
      .AsNoTracking()
      .ToListAsync();
    sw.Stop();
    logger.LogInformation("GetObservations: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);
    return observations;
  }
}
