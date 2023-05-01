using HajurKoCarRental.Client.Core.Models;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Models.ResponseModels;

namespace HajurKoCarRental.Client.Core.Blood;

public class BloodState
{
    internal User? User { get; set; }
    internal PermissionsModel? Permissions { get; set; }
    internal Dimension? Dimension { get; set; }
}