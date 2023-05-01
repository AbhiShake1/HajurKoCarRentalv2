using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.RentalRequests.Views;

public class RentalsRequestsView : BaseView
{
    public List<RentalRequest> GetAllRentalRequests(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.RentalRequests.Skip(page * pageSize).Take(pageSize).ToList();
    }

    public RentalRequest GetRentalRequest(int id, HajurKoDbContext context)
    {
        var rentalRequest = context.RentalRequests.FirstOrDefault(request => request.Id == id);
        if (rentalRequest is null) throw new KeyNotFoundException($"Rental with ID {id} not found");
        return rentalRequest;
    }

    public async Task CreateRentalRequestAsync(RentalRequest rentalRequest, HajurKoDbContext context)
    {
        await context.RentalRequests.AddAsync(rentalRequest);
        await context.SaveChangesAsync();
    }

    public async Task UpdateRentalRequestAsync(RentalRequest rentalRequest, HajurKoDbContext context)
    {
        context.Entry(rentalRequest).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteRentalRequestAsync(int id, HajurKoDbContext context)
    {
        var rentalRequest = await context.RentalRequests.FirstOrDefaultAsync(request => request.Id == id);
        if (rentalRequest == null) throw new KeyNotFoundException($"Rental with ID {id} not found");

        context.RentalRequests.Remove(rentalRequest);
        await context.SaveChangesAsync();
    }
}