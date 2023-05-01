using System.Text.Json;
using HajurKoCarRental.Client.Helpers;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Core.Base;

public class BaseDao
{
    private readonly IJSRuntime _runtime;

    protected BaseDao(IJSRuntime runtime)
    {
        _runtime = runtime;
    }
    
    protected async Task<T?> GetAsync<T>(string key)
    {
        var json = await _runtime.InvokeAsync<string>("localStorage.getItem", key);

        return json is null ? default : JsonSerializer.Deserialize<T>(json);
    }

    protected async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await _runtime.InvokeVoidAsync("localStorage.setItem", key, json);
    }
    
    protected async Task DeleteAsync(string key)
    {
        await _runtime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task<T?> Resolver<T>(
        Func<Task<T>> onNetwork,
        bool forceCached = false,
        Func<Task<T>>? onCache = null,
        Func<T, Task>? saveCache = null
    )
    {
        T? result;

        if (forceCached || !NetworkHelper.IsInternetAvailable())
        {
            result = await onCache?.Invoke();
            if (result is not null) return result;
        }

        result = await onNetwork();

        if (result is null) return default;
        await saveCache?.Invoke(result);
        return result;

    }
}