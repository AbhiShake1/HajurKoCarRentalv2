using HajurKoCarRental.Server.Lib.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Blood;

static class DatabaseBlood
{
    public static void InitializeDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("Default");
        // var serverVersion = new MySqlServerVersion(new Version(8, 0, 23));
        builder.Services.AddDbContext<HajurKoDbContext>(options => options
            .UseSqlServer(connectionString)
            // .UseNpgsql(connectionString)
            // .UseMySql(connectionString, serverVersion)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors()
        );
        builder.Services.AddEndpointsApiExplorer();
        
    }
}