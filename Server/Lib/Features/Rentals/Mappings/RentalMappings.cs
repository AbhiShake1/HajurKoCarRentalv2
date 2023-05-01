using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Rentals.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Rentals.Mappings;

class RentalMappings : BaseMappings<RentalsView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapGet(ApiRoutes.RentalsRoute, View.GetAllRentals);
        app.MapPost(ApiRoutes.RentalsRoute, View.CreateRentalAsync);
        app.MapGet(ApiRoutes.RentalRoute, View.GetRental);
        app.MapPut(ApiRoutes.RentalRoute, View.UpdateRentalAsync);
        app.MapDelete(ApiRoutes.RentalRoute, View.DeleteRentalAsync);
    }
}