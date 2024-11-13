namespace ApiTest.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Refit;

public interface IControllerApiClient
{
  [Headers("Accept: text/plain, application/json, text/json")]
  [Get("/")]
  Task<IApiResponse<ObservationsResponse>> GetObservations(CancellationToken cancellationToken = default);

  [Headers("Accept: text/plain, application/json, text/json")]
  [Get("/{date}")]
  Task<IApiResponse<ObservationResponse>> GetByDate(DateTimeOffset date, CancellationToken cancellationToken = default);

  [Headers("Accept: text/plain, application/json, text/json")]
  [Get("/dates")]
  Task<IApiResponse<DatesResponse>> GetMeasuredDates(CancellationToken cancellationToken = default);
}
