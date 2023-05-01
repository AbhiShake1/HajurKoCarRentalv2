using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.RentalRequests.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.RentalRequests.Mappings;

class RentalRequestsMappings : BaseMappings<RentalsRequestsView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapGet(ApiRoutes.RentalRequestsRoute, View.GetAllRentalRequests);
        app.MapPost(ApiRoutes.RentalRequestsRoute, View.CreateRentalRequestAsync);
        app.MapGet(ApiRoutes.RentalRequestRoute, View.GetRentalRequest);
        app.MapPut(ApiRoutes.RentalRequestsRoute, View.UpdateRentalRequestAsync);
        app.MapDelete(ApiRoutes.RentalRequestsRoute, View.DeleteRentalRequestAsync);
    }
}