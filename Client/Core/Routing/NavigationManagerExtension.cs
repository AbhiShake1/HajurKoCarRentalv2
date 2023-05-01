using Microsoft.AspNetCore.Components;

namespace HajurKoCarRental.Client.Core.Routing;

public static class NavigationManagerExtensions
{
    public static bool GetIsProtected(this NavigationManager navigationManager)
    {
        var route = navigationManager.Uri;
        var baseRoute = navigationManager.BaseUri;
        return !(route.Contains(Routes.Login) || route.Contains(Routes.Signup) || route.Equals(baseRoute));
    }
}