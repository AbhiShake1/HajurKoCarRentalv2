using System.Reflection;
using HajurKoCarRental.Client.Core.Base;

namespace HajurKoCarRental.Client.Extensions;

public static class RepositoryInjectionExtension
{
    public static void AddHajurKoRepositories(this IServiceCollection services)
    {
        var repositoryTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type is { IsAbstract: false, IsClass: true, BaseType.IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<,>));
        
        foreach (var type in repositoryTypes)
        {
            services.AddScoped(type);
        }
    }
}