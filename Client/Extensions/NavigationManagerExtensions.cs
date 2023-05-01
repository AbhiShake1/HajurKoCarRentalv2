namespace HajurKoCarRental.Client.Extensions;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

public static class NavigationManagerExtensions
{
    public static async Task GoBackAsync(this NavigationManager navigationManager, IJSRuntime jsRuntime)
    {
        await jsRuntime.InvokeVoidAsync("history.back");
    }
}
