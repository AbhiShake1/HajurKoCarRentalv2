using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Shared.Models.RequestModels;
using HajurKoCarRental.Shared.Models.ResponseModels;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Client.Features.Auth.Data;

internal class AuthService : BaseService
{
    public Task<LoginResponseModel> Login(LoginRequestModel model)
    {
        return Client.PostAsync<LoginResponseModel>(ApiRoutes.LoginRoute, model);
    }
    
    public Task<SignupResponseModel> Register(SignupRequestModel model)
    {
        return Client.PostAsync<SignupResponseModel>(ApiRoutes.RegisterRoute, model);
    }

    public AuthService(HajurKoHttpClient client) : base(client)
    {
    }

}