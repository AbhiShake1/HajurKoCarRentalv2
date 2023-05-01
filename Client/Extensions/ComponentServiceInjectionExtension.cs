using HajurKoCarRental.Client.Core.Blood;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Client.Shared.HajurKoComponents.ComponentServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Extensions;

internal static class ComponentServiceInjectionExtension
{
   public static void AddHajurKoServices(this IServiceCollection services)
   {
      // services.AddScoped<HajurKoViewportService>();
      services.AddScoped<HajurKoDialogService>();
   }
}