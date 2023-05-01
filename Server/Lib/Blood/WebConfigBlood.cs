using System.Text.Json;

namespace HajurKoCarRental.Server.Lib.Blood;

static class WebConfigBlood
{
    public static void InitializeWebConfigs(this IServiceCollection services)
    {
        services.ConfigureHttpJsonOptions(options =>
        {
            // to not change name
            options.SerializerOptions.PropertyNamingPolicy = null;
        });
        
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", cors =>
            {
                cors
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }
}