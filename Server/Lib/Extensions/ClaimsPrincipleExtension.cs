using System.Text.RegularExpressions;
using HajurKoCarRental.Server.Lib.Core.Models;
using HajurKoCarRental.Shared.Models.DataModels;
using HajurKoCarRental.Shared.Models.ResponseModels;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Extensions;
// Add this namespace

internal static partial class Permissions
{
    public static bool IsEndpointAccessible(UserType userType, string endpoint, string method)
    {
        if(userType.Equals(UserType.Admin))
        {
            // Admins can access all endpoints
            return true;
        }

        var userAccess = GetAccess();
        var userEndpoints = userAccess.AccessibleEndpoints[userType];
        
        var endpointMethod = new AccessibleEndpoint(
            Regex.Replace(endpoint, @"/\d+(/)?", "/{id}"),
            method.Equals(HttpMethods.Get),
            method.Equals(HttpMethods.Post),
            method.Equals(HttpMethods.Put),
            method.Equals(HttpMethods.Delete)
        );
        return userEndpoints.Contains(endpointMethod);
    }

    public static PermissionsModel GetPermissions(UserType userType)
    {
        return new PermissionsModel(
            CanGetCars: IsEndpointAccessible(userType, ApiRoutes.CarsRoute, HttpMethods.Get),
            CanCreateCars: IsEndpointAccessible(userType, ApiRoutes.CarsRoute, HttpMethods.Post),
            CanUpdateCars: IsEndpointAccessible(userType, ApiRoutes.CarRoute, HttpMethods.Post),
            CanDeleteCar: IsEndpointAccessible(userType, ApiRoutes.CarRoute, HttpMethods.Delete),
            CanGetCar: IsEndpointAccessible(userType, ApiRoutes.CarRoute, HttpMethods.Get),
            CanGetRentalRequests: IsEndpointAccessible(userType, ApiRoutes.RentalRequestsRoute, HttpMethods.Get),
            CanCreateRentalRequests: IsEndpointAccessible(userType, ApiRoutes.RentalRequestsRoute, HttpMethods.Post),
            CanUpdateRentalRequest: IsEndpointAccessible(userType, ApiRoutes.RentalRequestRoute, HttpMethods.Put),
            CanDeleteDeleteRequest: IsEndpointAccessible(userType, ApiRoutes.RentalRequestRoute, HttpMethods.Delete),
            CanGetRentals: IsEndpointAccessible(userType, ApiRoutes.RentalsRoute, HttpMethods.Get),
            CanCreateRentals: IsEndpointAccessible(userType, ApiRoutes.RentalsRoute, HttpMethods.Post),
            CanGetRental: IsEndpointAccessible(userType, ApiRoutes.RentalRoute, HttpMethods.Get),
            CanUpdateRental: IsEndpointAccessible(userType, ApiRoutes.RentalRoute, HttpMethods.Put),
            CanDeleteRental: IsEndpointAccessible(userType, ApiRoutes.RentalRoute, HttpMethods.Delete),
            CanGetUsers: IsEndpointAccessible(userType, ApiRoutes.UsersRoute, HttpMethods.Get),
            CanCreateUsers: IsEndpointAccessible(userType, ApiRoutes.UsersRoute, HttpMethods.Post),
            CanGetUser: IsEndpointAccessible(userType, ApiRoutes.UserRoute, HttpMethods.Get),
            CanUpdateUser: IsEndpointAccessible(userType, ApiRoutes.UserRoute, HttpMethods.Put),
            CanDeleteUser: IsEndpointAccessible(userType, ApiRoutes.UserRoute, HttpMethods.Delete),
            CanGetCustomers: IsEndpointAccessible(userType, ApiRoutes.CustomersRoute, HttpMethods.Get),
            CanUpdateCustomer: IsEndpointAccessible(userType, ApiRoutes.CustomerRoute, HttpMethods.Post),
            CanDeleteCustomer: IsEndpointAccessible(userType, ApiRoutes.CustomerRoute, HttpMethods.Delete),
            CanGetCustomer: IsEndpointAccessible(userType, ApiRoutes.CustomerRoute, HttpMethods.Get),
            CanGetPayments: IsEndpointAccessible(userType, ApiRoutes.PaymentsRoute, HttpMethods.Get),
            CanCreatePayments: IsEndpointAccessible(userType, ApiRoutes.PaymentsRoute, HttpMethods.Post),
            CanGetDamages: IsEndpointAccessible(userType, ApiRoutes.DamagesRoute, HttpMethods.Get),
            CanCreateDamages: IsEndpointAccessible(userType, ApiRoutes.DamagesRoute, HttpMethods.Post),
            CanGetDamage: IsEndpointAccessible(userType, ApiRoutes.DamageRoute, HttpMethods.Get),
            CanUpdateDamage: IsEndpointAccessible(userType, ApiRoutes.DamageRoute, HttpMethods.Put),
            CanLogin: IsEndpointAccessible(userType, ApiRoutes.LoginRoute, HttpMethods.Post),
            CanRegister: IsEndpointAccessible(userType, ApiRoutes.RegisterRoute, HttpMethods.Post),
            CanForgetPassword: IsEndpointAccessible(userType, ApiRoutes.ForgotPasswordRoute, HttpMethods.Post),
            CanResetPassword: IsEndpointAccessible(userType, ApiRoutes.ResetPasswordRoute, HttpMethods.Post),
            CanGetOffer: IsEndpointAccessible(userType, ApiRoutes.OfferRoute, HttpMethods.Get),
            CanGetOffers: IsEndpointAccessible(userType, ApiRoutes.OffersRoute, HttpMethods.Get),
            CanCreateOffers: IsEndpointAccessible(userType, ApiRoutes.OffersRoute, HttpMethods.Post),
            CanGetPayment: IsEndpointAccessible(userType, ApiRoutes.PaymentRoute, HttpMethods.Get),
            CanDownloadDocuments: IsEndpointAccessible(userType, ApiRoutes.DocumentsDownloadRoute, HttpMethods.Get),
            CanUploadDocuments: IsEndpointAccessible(userType, ApiRoutes.DocumentsUploadRoute, HttpMethods.Post),
            CanGetStaffs: IsEndpointAccessible(userType, ApiRoutes.StaffsRoute, HttpMethods.Get)
        );
    }

    private static UserAccess GetAccess()
    {
        var access = new Dictionary<UserType, AccessibleEndpoint[]>
        {
            { UserType.Admin, _getAdminEndpoints() },
            { UserType.Staff, _getStaffEndpoints() },
            { UserType.Client, _getClientEndpoints() },
            { UserType.Anonymous, _getAnonymousEndpoints() }
        };
        return new UserAccess(access);
    }

    private static AccessibleEndpoint[] _getAnonymousEndpoints()
    {
        return new[]
        {
            new AccessibleEndpoint(ApiRoutes.LoginRoute, Post: true),
            new(ApiRoutes.RegisterRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.CarsRoute, true),
            new AccessibleEndpoint(ApiRoutes.CarRoute, true),
            new("/", true),
            // TODO (AbhiShake1): Remove swagger access later
            new AccessibleEndpoint("/swagger/index.html", true),
            new AccessibleEndpoint("/service-worker.js", true),
            new AccessibleEndpoint("/swagger/v1/swagger.json", true)
            //swagger end
        };
    }

    private static AccessibleEndpoint[] _getClientEndpoints()
    {
        return new[]
        {
            new AccessibleEndpoint(ApiRoutes.LoginRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.LogoutRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.CarRoute, true),
            new AccessibleEndpoint(ApiRoutes.CarsRoute, true),
            // TODO (AbhiShake1): Remove swagger access later
            new AccessibleEndpoint("/swagger/index.html", true),
            new AccessibleEndpoint("/service-worker.js", true),
            new AccessibleEndpoint("/swagger/v1/swagger.json", true),
            //swagger end
            new("/", true)
        };
    }

    private static AccessibleEndpoint[] _getStaffEndpoints()
    {
        return new[]
        {
            new AccessibleEndpoint(ApiRoutes.LoginRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.LogoutRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.CarsRoute, true),
            new AccessibleEndpoint(ApiRoutes.CarRoute, true),
            new("/", true),
            new(ApiRoutes.RegisterRoute, Post: true)
        };
    }

    private static AccessibleEndpoint[] _getAdminEndpoints()
    {
        return new[]
        {
            new AccessibleEndpoint(ApiRoutes.LoginRoute, Post: true),
            new(ApiRoutes.RegisterRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.LogoutRoute, Post: true),
            new AccessibleEndpoint(ApiRoutes.RentalRequestsRoute, true, true),
            new AccessibleEndpoint(ApiRoutes.RentalRequestRoute, Put: true, Delete: true),
            new(ApiRoutes.StaffsRoute, true),
            new("/", true)
        };
    }

    [GeneratedRegex("/\\d+/")]
    private static partial Regex EndpointMatcher();
}