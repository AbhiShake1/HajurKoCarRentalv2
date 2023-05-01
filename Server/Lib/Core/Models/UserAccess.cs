using HajurKoCarRental.Shared.Models.DataModels;

namespace HajurKoCarRental.Server.Lib.Core.Models;

internal record UserAccess(Dictionary<UserType, AccessibleEndpoint[]> AccessibleEndpoints)
{
    // public static UserAccess Empty => new(new Dictionary<UserType, AccessibleEndpoint[]>());
}

internal record AccessibleEndpoint(string Endpoint, bool Get = false, bool Post = false, bool Put = false, bool Delete = false);

