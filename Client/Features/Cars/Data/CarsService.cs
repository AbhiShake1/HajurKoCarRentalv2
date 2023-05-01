using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Client.Extensions;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Client.Features.Cars.Data;

public class CarsService : BaseService
{
    protected CarsService(HajurKoHttpClient client) : base(client)
    {
    }

    public Task<Car[]> GetAllCars()
    {
        return Client.GetAsync<Car[]>(ApiRoutes.CarsRoute);
    }
    
    public Task<Car> GetCar(int id)
    {
        return Client.GetAsync<Car>(ApiRoutes.CarRoute, id);
    }
}