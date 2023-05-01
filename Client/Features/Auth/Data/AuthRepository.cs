using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.HajurKoServices;
using HajurKoCarRental.Shared.Models.RequestModels;
using HajurKoCarRental.Shared.Models.ResponseModels;
using HajurKoCarRental.Shared.Types;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Auth.Data;

internal class AuthRepository : BaseRepository<AuthService, AuthDao>
{
    public AuthRepository(HajurKoHttpClient httpClient, IJSRuntime runtime) : base(httpClient, runtime)
    {
    }

    public Task<LoginResponseModel?> GetUser()
    {
        return Dao.GetUser();
    }

    public Task DeleteUser()
    {
        return Dao.DeleteUser();
    }

    public async Task<Either<string, LoginResponseModel?>> Login(LoginRequestModel model)
    {
        try
        {
            var res = await Dao.Resolver(
               onNetwork: async () => await Service.Login(model),
               saveCache: (user) => Dao.SaveUser(user)
            );
            return Either<string, LoginResponseModel?>.Right(res);
        }
        catch (Exception ex)
        {
            return Either<string, LoginResponseModel?>.Left(ex.Message);
        }
    }

    public async Task<Either<string, SignupResponseModel?>> Register(SignupRequestModel model)
    {
        try
        {
            var res = await Service.Register(model);
            return Either<string, SignupResponseModel?>.Right(res);
        }
        catch (Exception ex)
        {
            return Either<string, SignupResponseModel?>.Left(ex.Message);
        }
    }
}