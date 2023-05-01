using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.AspNetCore.Identity;

namespace HajurKoCarRental.Server.Lib.Extensions;

internal static class AuthMiddlewareExtension
{
    public static void UsePermissionMiddleware(this WebApplication app)
    {
        app.Use((context, next) =>
        {
            var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
            var user = userManager.GetUserAsync(context.User);
            
            if (Permissions.IsEndpointAccessible(
                    user.Result?.UserType ?? UserType.Anonymous,
                    context.Request.Path,
                    context.Request.Method
                )
               )
            {
                return next(context);
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return Task.CompletedTask;
        });
    }
}