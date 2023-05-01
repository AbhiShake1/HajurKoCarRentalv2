using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Extensions;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Models.RequestModels;
using HajurKoCarRental.Shared.Models.ResponseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HajurKoCarRental.Server.Lib.Features.Authentication.Views;

public class AuthenticationView : BaseView
{
    public async Task<IResult> Register(
        HttpContext context,
        // IValidator<SignupRequestModel> validator,
        [Required] [FromBody] SignupRequestModel model,
        [FromServices] UserManager<User> userManager,
        [FromServices] SignInManager<User> signinManager
    )
    {
        // var validation = await validator.ValidateAsync(model);
        // if (!validation.IsValid)
        // {
        //     var errors = validation.Errors
        //         .GroupBy(x => x.PropertyName)
        //         .ToDictionary(
        //             g => g.Key,
        //             g => g.Select(x => x.ErrorMessage).ToArray()
        //         );
        //     return Results.ValidationProblem(errors);
        // }

        User user = new()
        {
            UserName = model.Username,
            Email = model.Email,
            UserType = UserType.Client // default to client
        };

        // Check if the user is authenticated
        if (context.User.Identity?.IsAuthenticated ?? false)
        {
            // Get the current user
            var currentUser = await userManager.GetUserAsync(context.User);
            
            if (currentUser?.UserType == UserType.Admin)
                // If the current user is an admin, signup staff
                user.UserType = UserType.Staff;
            else
                // If the current user is not an admin, dont allow
                return Results.Unauthorized();
        }

        // Create the user
        var result = await userManager.CreateAsync(user, model.Password);
        
        return !result.Succeeded
            ? Results.BadRequest(new { message = string.Join("\n", result.Errors.Select(e => e.Description)) })
            : Results.Ok(new SignupResponseModel("User created successfully."));
        // Results.Ok(new SignupResponseModel("User created successfully."));
    }
    
    public string ResetPassword(ResetPasswordRequestModel model)
    {
        return $"Reset-{model.OldPassword}";
    }
    
    public async Task<string> Logout([FromServices] SignInManager<User> signInManager)
    {
        await signInManager.SignOutAsync();
        return "Logged out";
    }
    
    public async Task<IResult> Login(
        HttpContext context,
        [Required] [FromBody] LoginRequestModel model,
        [FromServices] SignInManager<User> signInManager,
        [FromServices] UserManager<User> userManager,
        [FromServices] IConfiguration configuration
    )
    {
        var user = await signInManager.UserManager.FindByEmailAsync(model.Email);
        
        if (user is null) return Results.BadRequest("Invalid email");
        
        var result = await signInManager.PasswordSignInAsync(user, model.Password, true, false);
        
        if (!result.Succeeded) return Results.BadRequest("Invalid login attempt");
        
        var permissions = Permissions.GetPermissions(user.UserType);
        var token = GenerateToken(user, configuration);
        return Results.Ok(new LoginResponseModel(
                token,
                user,
                permissions
            )
        );
    }

    private string GenerateToken(User user, IConfiguration configuration)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            configuration["JWT:ValidIssuer"],
            configuration["JWT:ValidAudience"],
            claims,
            expires: DateTime.Now.AddDays(365),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}