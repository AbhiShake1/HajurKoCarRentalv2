using HajurKoCarRental.Client.Core.Base;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Staffs.Data;

public class StaffsDao : BaseDao
{
    protected StaffsDao(IJSRuntime runtime) : base(runtime)
    {
    }
}