using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Users.Views;

public class UsersView : BaseView
{
    public List<User> GetAllUsers(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Users
            .Where(u=>u.UserType.Equals(UserType.Client))
            .Skip(page * pageSize).Take(pageSize).ToList();
    }

    public List<User> GetAllStaffs(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Users.Where(u => u.UserType == UserType.Staff)
            .Skip(page * pageSize).Take(pageSize).ToList();
    }

    public User GetUser(string id, HajurKoDbContext context)
    {
        var user = context.Users.FirstOrDefault(user => user.Id.Equals($"{id}"));
        if (user is null) throw new KeyNotFoundException($"User with ID {id} not found");
        return user;
    }

    public async Task CreateUserAsync(User user, HajurKoDbContext context)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user, HajurKoDbContext context)
    {
        context.Entry(user).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(string id, HajurKoDbContext context)
    {
        var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id.ToString());
        if (user == null) throw new KeyNotFoundException($"User with ID {id} not found");

        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}