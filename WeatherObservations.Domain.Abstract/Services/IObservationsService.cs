namespace WeatherObservations.Domain.Abstract.Services;

using WeatherObservations.Domain.Abstract.Model;

public interface IObservationsService
{
  Task<bool> IsEmpty();
  Task<IEnumerable<DateTimeOffset>> GetMeasuredDates();
  Task AddObservation(Observation observation, bool updateExisting);
  Task AddObservations(IEnumerable<Observation> observations, bool updateExisting);
  Task AddObservationsSlow(IEnumerable<Observation> observations, bool updateExisting);
  Task UpdateObservation(Observation observation);
  Task UpdateObservations(IEnumerable<Observation> observations);
  Task<Observation?> GetObservation(int id);
  Task<Observation?> GetObservation(DateTimeOffset date);
  Task<IEnumerable<Observation>> GetObservations();
  Task ClearTable();
  Task CreateDb();
}
