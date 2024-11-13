namespace ApiTest.Services;
using System.Threading;
using System.Threading.Tasks;

using Refit;

public partial interface IFastEndpointApiClient
{
  [Headers("Accept: application/json, application/problem+json")]
  [Get("/observations")]
  Task<IApiResponse<ObservationsResponse>> GetObservations(CancellationToken cancellationToken = default);

  [Headers("Accept: application/json, application/problem+json")]
  [Get("/observations/{date}")]
  Task<IApiResponse<ObservationResponse>> GetObservationByDate(System.DateTimeOffset date, CancellationToken cancellationToken = default);

  [Headers("Accept: application/json, application/problem+json")]
  [Get("/observations/dates")]
  Task<IApiResponse<DatesResponse>> GetMeasuredDates(CancellationToken cancellationToken = default);
}

