using System.Net.Http.Headers;
using BlazorAnimation;
using HajurKoCarRental.Client;
using HajurKoCarRental.Client.Core.HajurKoServices;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using HajurKoCarRental.Client.Extensions;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(service => new HajurKoHttpClient(service.GetRequiredService<IJSRuntime>())
{
    BaseAddress = new Uri("https://localhost:5001/"),
});
builder.Services.AddMudServices();
builder.Services.AddHajurKoRepositories();
builder.Services.AddHajurKoServices();
builder.Services.Configure<AnimationOptions>(anim =>
{
    anim.Enabled = true;
    anim.Speed = TimeSpan.FromMilliseconds(500);
    anim.Effect = Effect.FadeIn;
    anim.Delay = TimeSpan.Zero;
    anim.IterationCount = 1;
});

await builder.Build().RunAsync();