using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Routing;
using HajurKoCarRental.Shared.Types;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Cars.Data;

public class CarsRepository : BaseRepository<CarsService, CarsDao>
{
    public CarsRepository(HajurKoHttpClient httpClient, IJSRuntime runtime) : base(httpClient, runtime)
    {
    }
    
    public async Task<Either<string, Car[]>> GetAllCars()
    {
        try
        {
            var res = await Service.GetAllCars();
            return Either<string, Car[]>.Right(res);
        }
        catch (Exception e)
        {
            return Either<string, Car[]>.Left(e.Message);
        }
    }
    
    public async Task<Either<string, Car>> GetCar(int carId)
    {
        try
        {
            var res = await Service.GetCar(carId);
            return Either<string, Car>.Right(res);
        }
        catch (Exception e)
        {
            return Either<string, Car>.Left(e.Message);
        }
    }
}