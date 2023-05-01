using Microsoft.OpenApi.Models;

namespace HajurKoCarRental.Server.Lib.Blood;

static class SwaggerBlood
{
    public static void InitializeSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HajurKoCarRental API", Version = "v1" });
        });
    }
}