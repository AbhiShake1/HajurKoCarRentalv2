using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Client.Features.Cars.Data;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Types;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Staffs.Data;

public class StaffsRepository : BaseRepository<StaffsService, StaffsDao>
{
    public StaffsRepository(HajurKoHttpClient httpClient, IJSRuntime runtime) : base(httpClient, runtime)
    {
    }

    public async Task<Either<string, User[]>> GetAllStaffs()
    {
        try
        {
            var res = await Service.GetAllStaffs();
            return Either<string, User[]>.Right(res);
        }
        catch (Exception e)
        {
            return Either<string, User[]>.Left(e.Message);
        }
    }
    
    public async Task<Either<string, string>> DeleteStaff(string staffId)
    {
        try
        {
            await Service.DeleteStaff(staffId);
            return Either<string, string>.Right("Staff deleted");
        }
        catch (Exception e)
        {
            return Either<string, string>.Left(e.Message);
        }
    }
}