using HajurKoCarRental.Server.Lib.Blood;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Server.Lib.Extensions;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Routing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeDependencies();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.UsePermissionMiddleware();
app.AddDevHelperPages();
app.AddAppMappings();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<HajurKoDbContext>();
//     var userManager = services.GetRequiredService<UserManager<User>>();
//
//     // Ensure that the database is created and migrated to the latest version
//     context.Database.Migrate();
//     
//     if (!context.Users.Any(u=>u.UserType.Equals(UserType.Admin)))
//     {
//         // Add an item to the database if it doesn't already exist
//         var admin = new User
//         {
//             // Address = "virtual",
//             UserName = "admin",
//             UserType = UserType.Admin,
//             Email = "admin@gmail.com"
//         };
//         await userManager.CreateAsync(admin, "Admin12345");
//     }
// }

app.Run();