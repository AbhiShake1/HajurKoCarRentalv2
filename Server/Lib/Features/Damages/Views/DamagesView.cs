using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Damages.Views;

public class DamagesView : BaseView
{
    public List<Damage> GetAllDamages(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        var cars = context.Cars;
        return context.Damages
            .Include(damage => cars.FirstOrDefault(car => car.Id == damage.CarId))
            .Skip(page * pageSize).Take(pageSize).ToList();
    }

    public Damage GetDamage(int id, HajurKoDbContext context)
    {
        var cars = context.Cars;
        var damage = context.Damages
            .Include(damage => cars.FirstOrDefault(car => car.Id == damage.CarId))
            .FirstOrDefault(damage => damage.Id == id);
        if (damage is null) throw new KeyNotFoundException($"Damage with ID {id} not found");
        return damage;
    }

    public async Task CreateDamageAsync(Damage damage, HajurKoDbContext context)
    {
        await context.Damages.AddAsync(damage);
        await context.SaveChangesAsync();
    }

    public async Task UpdateDamageAsync(Damage damage, HajurKoDbContext context)
    {
        context.Entry(damage).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteDamageAsync(int id, HajurKoDbContext context)
    {
        var damage = await context.Damages.FirstOrDefaultAsync(damage => damage.Id == id);
        if (damage == null) throw new KeyNotFoundException($"Damage with ID {id} not found");

        context.Damages.Remove(damage);
        await context.SaveChangesAsync();
    }
}