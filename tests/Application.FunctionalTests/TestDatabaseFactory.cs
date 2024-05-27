using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkIo.Application.FunctionalTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("TestDefaultConnection");

        if (!connectionString.IsNullOrEmpty())
        {

            var database = new SqlServerTestDatabase();

            await database.InitialiseAsync();

            return database;
        }
        else
        {
            var database = new TestcontainersTestDatabase();

            await database.InitialiseAsync();

            return database;
        }

    }
}
