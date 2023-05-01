using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Offers.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Offers.Mappings;

class OfferMappings : BaseMappings<OffersView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapGet(ApiRoutes.OffersRoute, View.GetAllOffers);
        app.MapPost(ApiRoutes.OffersRoute, View.CreateOfferAsync);
        app.MapGet(ApiRoutes.OfferRoute, View.GetOffer);
    }
}