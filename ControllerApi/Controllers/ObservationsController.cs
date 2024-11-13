namespace ControllerApi.Controllers;

using System.Threading.Tasks;

using ControllerApi;

using Microsoft.AspNetCore.Mvc;

using WeatherObservations.Domain.Abstract.Model;
using WeatherObservations.Domain.Abstract.Services;

[ApiController]
[Route("[controller]")]
public class ObservationsController(ILogger<ObservationsController> logger, IObservationsService service) 
  : ControllerBase
{

  [HttpGet("/", Name = "GetObservations")]
  public async Task<IEnumerable<Observation>> GetObservationsAsync()
  {
    var response = await service.GetObservations();
    return response;
  }

  [HttpGet("/{date}", Name = "GetByDate")]
  public async Task<Observation> GetByDate(DateTimeOffset date)
  {
    var response = await service.GetObservation(date);
    return response;
  }

  [HttpGet("/dates", Name = "GetMeasuredDates")]
  public async Task<IEnumerable<DateTimeOffset>> GetMeasuredDatesAsync()
  {
    var response = await service.GetMeasuredDates();
    return response;
  }
}
