using Refit;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ApiTest.Services
{
    public partial interface IMinimalApiClient
    {
        [Headers("Accept: application/json")]
        [Get("/observations")]
        Task<IApiResponse<ICollection<ObservationResponse>>> GetObservations(CancellationToken cancellationToken = default);

        [Headers("Accept: application/json")]
        [Get("/observations/{date}")]
        Task<IApiResponse<ObservationResponse>> GetByDate(System.DateTimeOffset date, CancellationToken cancellationToken = default);

        [Headers("Accept: application/json")]
        [Get("/observations/dates")]
        Task<IApiResponse<ICollection<DateTimeOffset>>> GetMeasuredDates(CancellationToken cancellationToken = default);
    }
}
