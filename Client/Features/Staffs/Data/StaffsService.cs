using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Client.Features.Staffs.Data;

public class StaffsService : BaseService
{
    protected StaffsService(HajurKoHttpClient client) : base(client)
    {
    }

    public Task<User[]> GetAllStaffs()
    {
        return Client.GetAsync<User[]>(ApiRoutes.StaffsRoute);
    }
    
    public Task DeleteStaff(string staffId)
    {
        return Client.DeleteAsync(ApiRoutes.UserRoute, staffId);
    }
}