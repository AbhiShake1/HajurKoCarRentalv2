using HajurKoCarRental.Shared.Models.DataModels;

namespace HajurKoCarRental.Shared.Models.ResponseModels;

public record LoginResponseModel(string Token, User User, PermissionsModel Permissions);

public record PermissionsModel(
    bool CanLogin,
    bool CanRegister,
    bool CanForgetPassword,
    bool CanResetPassword,
    bool CanUploadDocuments,
    bool CanDownloadDocuments,
    bool CanGetCars,
    bool CanCreateCars,
    bool CanUpdateCars,
    bool CanGetCar,
    bool CanDeleteCar,
    bool CanGetUsers,
    bool CanCreateUsers,
    bool CanGetUser,
    bool CanUpdateUser,
    bool CanDeleteUser,
    bool CanGetRentalRequests,
    bool CanCreateRentalRequests,
    bool CanDeleteDeleteRequest,
    bool CanUpdateRentalRequest,
    bool CanGetRentals,
    bool CanCreateRentals,
    bool CanGetRental,
    bool CanUpdateRental,
    bool CanDeleteRental,
    bool CanGetDamages,
    bool CanCreateDamages,
    bool CanGetDamage,
    bool CanUpdateDamage,
    bool CanGetPayments,
    bool CanCreatePayments,
    bool CanGetPayment,
    bool CanGetOffers,
    bool CanCreateOffers,
    bool CanGetOffer,
    bool CanGetCustomers,
    bool CanGetCustomer,
    bool CanUpdateCustomer,
    bool CanDeleteCustomer,
    bool CanGetStaffs
);