namespace DownloadClient.Services;

using System.Diagnostics;

using DownloadClient.Clients;
using DownloadClient.Contracts;

using Refit;

public class SmhiService(ILogger<SmhiService> logger, ISmhiObservationApiClient client)
{
  public async Task<IEnumerable<SmhiObservations>> GetObservations()
  {
    List<SmhiObservations> observations = [];
    Stopwatch sw = Stopwatch.StartNew();

    foreach (string parameter in new string[] { ObservationsParam.ParameterTemperature, ObservationsParam.ParameterHumidity, ObservationsParam.ParameterPressure })
    {
      ApiResponse<SmhiObservations> response = await client.GetObservationsForPeriod(ObservationsParam.Version, parameter, ObservationsParam.Station, ObservationsParam.Period);
      _ = await response.EnsureSuccessStatusCodeAsync();
      if (response.IsSuccessStatusCode)
      {
        observations.Add(response.Content);
      }
    }

    sw.Stop();
    logger.LogInformation("GetObservations: {ms} ms = {ticks} ticks", sw.ElapsedMilliseconds, sw.ElapsedTicks);

    return observations;
  }
}
