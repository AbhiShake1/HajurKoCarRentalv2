using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Offers.Views;

public class OffersView : BaseView
{
    public List<Offer> GetAllOffers(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Offers.Skip(page * pageSize).Take(pageSize).ToList();
    }

    public Offer GetOffer(int id, HajurKoDbContext context)
    {
        var offer = context.Offers.FirstOrDefault(offer => offer.Id == id);
        if (offer is null) throw new KeyNotFoundException($"Offer with ID {id} not found");
        return offer;
    }

    public async Task CreateOfferAsync(Offer offer, HajurKoDbContext context)
    {
        await context.Offers.AddAsync(offer);
        await context.SaveChangesAsync();
    }

    public async Task UpdateOfferAsync(Offer offer, HajurKoDbContext context)
    {
        context.Entry(offer).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteOfferAsync(int id, HajurKoDbContext context)
    {
        var offer = await context.Offers.FirstOrDefaultAsync(offer => offer.Id == id);
        if (offer == null) throw new KeyNotFoundException($"Offer with ID {id} not found");

        context.Offers.Remove(offer);
        await context.SaveChangesAsync();
    }
}