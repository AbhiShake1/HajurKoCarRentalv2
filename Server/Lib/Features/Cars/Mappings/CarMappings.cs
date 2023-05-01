using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Cars.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Cars.Mappings;

class CarMappings : BaseMappings<CarsView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapGet(ApiRoutes.CarsRoute, View.GetAllCars);
        app.MapGet(ApiRoutes.CarRoute, View.GetCar);
        app.MapPut(ApiRoutes.CarRoute, View.UpdateCar);
        app.MapDelete(ApiRoutes.CarRoute, View.DeleteCar);
        app.MapPost(ApiRoutes.CarsRoute, View.CreateCar);
    }
}