using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Authentication.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Authentication.Mappings;

public class AuthenticationMappings : BaseMappings<AuthenticationView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapPost(ApiRoutes.LoginRoute, View.Login);
        app.MapPost(ApiRoutes.RegisterRoute, View.Register);
        app.MapPost(ApiRoutes.ResetPasswordRoute, View.ResetPassword);
        app.MapPost(ApiRoutes.LogoutRoute, View.Logout);
    }
}