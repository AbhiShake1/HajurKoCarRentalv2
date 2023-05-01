using System.Text;
using HajurKoCarRental.Server.Lib.Core.Configs;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HajurKoCarRental.Server.Lib.Blood;

static class AuthBlood
{
    public static void InitializeAuth(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        services.AddIdentity<User, IdentityRole>(IdentityConfigs.SetConfig)
            .AddEntityFrameworkStores<HajurKoDbContext>()
            .AddDefaultTokenProviders();

        var config = builder.Configuration;
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = config["JWT:ValidIssuer"],
                    ValidAudience = config["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]!))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"abhi:{context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        var email = context.Principal?.Identity?.Name;
                        if(email is null) return Task.CompletedTask;
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<User>>();
                        var user = userManager.FindByEmailAsync(email).Result;
                        
                        if(user is null) return Task.CompletedTask;

                        // Update the user's last login time
                        user.LastSeenOn = DateTime.UtcNow;
                        userManager.UpdateAsync(user);
                        
                        return Task.CompletedTask;
                    }
                };
            });
        services.AddAuthorization();
        

        services.AddScoped<UserManager<User>>();
        services.AddScoped<SignInManager<User>>();
    }
}