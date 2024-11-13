namespace FastEndpointApi.GetObservationsByDate;

using FastEndpoints;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

internal sealed class ObservationsByDateRequest
{
  [FromRoute]
  public DateTimeOffset Date { get; set; }

  internal sealed class ObservationsByDateValidator : Validator<ObservationsByDateRequest>
  {
    public ObservationsByDateValidator()
    {
      RuleFor(x => x.Date).NotNull();
      RuleFor(x => x.Date).LessThan(DateTimeOffset.Now);
    }
  }
}
