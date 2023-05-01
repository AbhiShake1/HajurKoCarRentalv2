using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Users.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Users.Mappings;

internal class UserMappings : BaseMappings<UsersView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapGet(ApiRoutes.UsersRoute, View.GetAllUsers);
        app.MapGet(ApiRoutes.StaffsRoute, View.GetAllStaffs);
        app.MapPost(ApiRoutes.UserRoute, View.CreateUserAsync);
        app.MapGet(ApiRoutes.UserRoute, View.GetUser);
        app.MapPut(ApiRoutes.UserRoute, View.UpdateUserAsync);
        app.MapDelete(ApiRoutes.UserRoute, View.DeleteUserAsync);
    }
}