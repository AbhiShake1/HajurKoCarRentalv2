using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Rentals.Views;

public class RentalsView : BaseView
{
    public List<Rental> GetAllRentals(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Rentals.Skip(page * pageSize).Take(pageSize).ToList();
    }

    public Rental GetRental(int id, HajurKoDbContext context)
    {
        var rental = context.Rentals.FirstOrDefault(rental => rental.Id == id);
        if (rental is null) throw new KeyNotFoundException($"Rental with ID {id} not found");
        return rental;
    }

    public async Task CreateRentalAsync(Rental rental, HajurKoDbContext context)
    {
        await context.Rentals.AddAsync(rental);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRentalAsync(Rental rental, HajurKoDbContext context)
    {
        context.Entry(rental).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteRentalAsync(int id, HajurKoDbContext context)
    {
        var rental = await context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
        if (rental == null) throw new KeyNotFoundException($"Rental with ID {id} not found");

        context.Rentals.Remove(rental);
        await context.SaveChangesAsync();
    }
}