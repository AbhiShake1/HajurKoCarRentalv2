using HajurKoCarRental.Client.Core.Base;
using HajurKoCarRental.Client.Core.Const;
using HajurKoCarRental.Shared.Models.ResponseModels;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Features.Auth.Data;

internal class AuthDao : BaseDao
{
    public async Task SaveUser(LoginResponseModel user)
    {
        await SetAsync(PreferenceKeys.User, user);
    }

    public async Task<LoginResponseModel?> GetUser()
    {
        return await GetAsync<LoginResponseModel>(PreferenceKeys.User);
    }

    public async Task DeleteUser()
    {
        await DeleteAsync(PreferenceKeys.User);
    }

    public AuthDao(IJSRuntime runtime) : base(runtime)
    {
    }
}