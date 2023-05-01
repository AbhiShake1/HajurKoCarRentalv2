using HajurKoCarRental.Client.Core.Blood;
using HajurKoCarRental.Client.Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Core.HajurKoServices;

internal enum PlatformSize
{
    Desktop,
    Tablet,
    Mobile
}

internal class HajurKoViewportService
{
    private readonly IJSRuntime _runtime;
    private readonly CascadingValue<BloodState> _state;

    public HajurKoViewportService(IJSRuntime runtime, CascadingValue<BloodState> state)
    {
        _runtime = runtime;
        _state = state;
        StartListeningAsync();
    }

    public async Task StartListeningAsync()
    {
        var dimen = await GetDimensions();
        if (_state.Value is not null)
            _state.Value.Dimension = new Dimension(dimen.Width, dimen.Height);
        var module = await _runtime.InvokeAsync<IJSObjectReference>("import", "./screenSize.js");

        await module.InvokeVoidAsync("listenForSizeChanges", DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public void OnSizeChanged(int width, int height)
    {
        _state.Value!.Dimension = new Dimension(width, height);
    }

    public async Task<Dimension> GetDimensions()
    {
        return await _runtime.InvokeAsync<Dimension>("getDimensions");
    }

    public async Task<PlatformSize> GetPlatformSize()
    {
        var dimen = await GetDimensions();
        var width = dimen.Width;
        return width switch
        {
            >= 1024 => PlatformSize.Desktop,
            >= 768 => PlatformSize.Tablet,
            _ => PlatformSize.Mobile
        };
    }

    public async Task<bool> GetIsMobile()
    {
        return await GetPlatformSize() == PlatformSize.Mobile;
    }

    public async Task<bool> GetIsTablet()
    {
        return await GetPlatformSize() == PlatformSize.Tablet;
    }

    public async Task<bool> GetIsDesktop()
    {
        return await GetPlatformSize() == PlatformSize.Desktop;
    }
}