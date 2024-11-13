namespace WeatherObservations.Data.Context;

using Microsoft.EntityFrameworkCore;

using WeatherObservations.Domain.Abstract.Model;

public class ObservationsContext(DbContextOptions<ObservationsContext> options)
  : DbContext(options)
{
  public DbSet<Observation> Observations => Set<Observation>();

  protected override void OnModelCreating(ModelBuilder builder)
    => builder.ApplyConfigurationsFromAssembly(typeof(ObservationsContext).Assembly);

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .EnableDetailedErrors(false)
        .EnableSensitiveDataLogging(false);

  public void EnsureDbExists()
  {
    if (Database.GetPendingMigrations().Any())
    {
      Database.Migrate();
    }
  }
}