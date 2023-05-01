using HajurKoCarRental.Client.Shared.HajurKoComponents.Modals;
using HajurKoCarRental.Shared.Models.RequestModels;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HajurKoCarRental.Client.Shared.HajurKoComponents.ComponentServices;

internal class HajurKoDialogService
{
    private readonly IDialogService _dialog;

    public HajurKoDialogService(IDialogService dialog)
    {
        _dialog = dialog;
    }

    public async Task<bool> ShowAsync<T>(string contentText, string confirmText, Color color = Color.Default,
        string? title = null)
        where T : ComponentBase
    {
        var parameters = new DialogParameters
        {
            { "ContentText", contentText },
            { "ButtonText", confirmText },
            { "Color", color },
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            CloseOnEscapeKey = true,
            NoHeader = title is null
        };

        var res = _dialog.Show<T>(title, parameters, options);
        var result = await res.Result;
        return !result.Cancelled;
    }
    
    public async Task<SignupRequestModel?> ShowSignupAsync(string? title = null)
    {
        var parameters = new DialogParameters();

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Medium,
            CloseOnEscapeKey = true,
            NoHeader = title is null
        };

        var res = _dialog.Show<HajurKoSignupDialogModal>(title, parameters, options);
        var result = await res.Result;
        if (result.Cancelled) return default;
        return result.Data as SignupRequestModel;
    }

    public Task<bool> ShowDefaultAsync(string contentText, string confirmText, Color color = Color.Default,
        string? title = null)
    {
        return ShowAsync<HajurKoDialogModal>(contentText: contentText, confirmText: confirmText, color: color, title: title);
    }
}