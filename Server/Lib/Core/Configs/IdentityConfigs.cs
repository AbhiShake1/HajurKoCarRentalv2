using Microsoft.AspNetCore.Identity;

namespace HajurKoCarRental.Server.Lib.Core.Configs;

static class IdentityConfigs
{
    private static IdentityOptions? _options;
    
    public static void SetConfig(IdentityOptions options)
    {
        _options ??= options;
        _setAuthConfigs();
        _setLockoutConfigs();
        _setUserConfigs();
        _setSignInConfigs();
    }

    private static void _setSignInConfigs()
    {
        _options!.SignIn.RequireConfirmedAccount = false;
        _options.SignIn.RequireConfirmedEmail = false;
        _options.SignIn.RequireConfirmedPhoneNumber = false;
    }

    private static void _setUserConfigs()
    {
        _options!.User.RequireUniqueEmail = true;
    }

    private static void _setLockoutConfigs()
    {
        _options!.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        _options.Lockout.AllowedForNewUsers = true;
        _options.Lockout.MaxFailedAccessAttempts = 5;
    }

    private static void _setAuthConfigs()
    {
        _options!.Password.RequireDigit = true;
        _options.Password.RequiredLength = 8;
        _options.Password.RequireLowercase = true;
        _options.Password.RequireUppercase = true;
        _options.Password.RequiredUniqueChars = 0;
        _options.Password.RequireNonAlphanumeric = false;
    }
}