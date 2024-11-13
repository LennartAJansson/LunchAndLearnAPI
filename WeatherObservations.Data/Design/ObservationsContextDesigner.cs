namespace WeatherObservations.Data.Design;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using WeatherObservations.Data.Context;

public class ObservationsContextDesigner : IDesignTimeDbContextFactory<ObservationsContext>
{
  //If you have your dbcontext in a class library, you need to add the following to your csproj file:
  //<ItemGroup>
  //  <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="x.y.z" />
  //</ItemGroup>

  //You also need to implement the IDesignTimeDbContextFactory interface in a class like this.
  //That way this class library knows how to create the dbcontext in designtime.

  //Then you can run the following commands in the Package Manager Console:
  //Add-Migration -Name your-migration-name -Context ObservationsContext -Project WeatherObservations.Data -StartupProject WeatherObservations.Data
  //Update-Database -Context ObservationsContext -Project WeatherObservations.Data -StartupProject WeatherObservations.Data

  public ObservationsContext CreateDbContext(string[] args)
  {
    //Store your connectionstring outside your repository in a safe place.
    IConfiguration configuration = new ConfigurationBuilder()
      .AddUserSecrets<ObservationsContext>()
      .Build();

    string? connectionString = configuration.GetConnectionString("SmhiDb")
      ?? throw new ArgumentNullException(nameof(connectionString), "Value is null!");

    ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
    DbContextOptionsBuilder<ObservationsContext> optionsBuilder = new();
    
    var options = new DbContextOptionsBuilder<ObservationsContext>()
      .UseMySql(connectionString, serverVersion)
      .Options;

    return new ObservationsContext(options);
  }
}