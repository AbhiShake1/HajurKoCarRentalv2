namespace HajurKoCarRental.Server.Lib.Blood;

public static class Blood
{
    public static void InitializeDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddMvc();
        builder.Services.InitializeSwagger();
        builder.InitializeDatabase();
        builder.Services.InitializeWebConfigs();
        builder.InitializeAuth();
    }
}