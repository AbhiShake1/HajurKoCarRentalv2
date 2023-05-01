using HajurKoCarRental.Client.Core.Base;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Cars.Data;

public class CarsDao : BaseDao
{
    protected CarsDao(IJSRuntime runtime) : base(runtime)
    {
    }
}