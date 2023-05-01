using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using HajurKoCarRental.Client.Core.Const;
using HajurKoCarRental.Client.Extensions;
using HajurKoCarRental.Shared.Models.ResponseModels;
using Microsoft.JSInterop;

namespace HajurKoCarRental.Client.Core.HajurKoServices;

public class HajurKoHttpClient : HttpClient
{
    private readonly IJSRuntime _runtime;

    public HajurKoHttpClient(IJSRuntime runtime)
    {
        _runtime = runtime;
    }

    private async Task<string?> GetAccessTokenAsync()
    {
        // TODO (AbhiShake1): fix it - too slow
        var res = await _runtime.InvokeAsync<string>("localStorage.getItem", PreferenceKeys.User);
        if (res is null) return default;
        var resp = JsonSerializer.Deserialize<LoginResponseModel>(res);
        return resp!.Token;
    }

    public async Task<T> PostAsync<T>(string uri, object model)
    {
        HttpContent content;

        if (model is not IEnumerable<KeyValuePair<string, object>> maps)
        {
            var data = JsonSerializer.Serialize(model);
            content = new StringContent(data, Encoding.UTF8, "application/json");
        }
        else
        {
            var multipartContent = new MultipartFormDataContent();
            foreach (var map in maps)
                if (map.Value is byte[] fileBytes)
                    multipartContent.Add(new ByteArrayContent(fileBytes), "file", "file");
                else
                    multipartContent.Add(new StringContent(JsonSerializer.Serialize(map.Value), Encoding.UTF8),
                        map.Key);
            content = multipartContent;
        }

        var accessToken = await GetAccessTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = content
        };
        if (!string.IsNullOrWhiteSpace(accessToken))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(errorResponse)) errorResponse = "This resource is not accessible";

            throw new HttpRequestException(errorResponse);
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse)!;
    }

    public async Task<T> PutAsync<T>(string uri, object id, object model)
    {
        HttpContent content;

        if (model is not IEnumerable<KeyValuePair<string, object>> maps)
        {
            content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
        }
        else
        {
            var multipartContent = new MultipartFormDataContent();
            foreach (var map in maps)
                if (map.Value is byte[] fileBytes)
                {
                    var fileExtension = Path.GetExtension(map.Key);
                    switch (fileExtension)
                    {
                        case ".pdf":
                            multipartContent.Add(new ByteArrayContent(fileBytes), map.Key, "application/pdf");
                            break;
                        case ".png":
                            multipartContent.Add(new ByteArrayContent(fileBytes), map.Key, "image/png");
                            break;
                        case ".jpg":
                            multipartContent.Add(new ByteArrayContent(fileBytes), map.Key, "image/jpeg");
                            break;
                    }
                }
                else
                {
                    multipartContent.Add(new StringContent(JsonSerializer.Serialize(map.Value), Encoding.UTF8),
                        map.Key);
                }

            content = multipartContent;
        }

        var accessToken = await GetAccessTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Put, uri)
        {
            Content = content
        };
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(errorResponse)) errorResponse = "This resource is not accessible";

            throw new HttpRequestException(errorResponse);
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse)!;
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        var accessToken = await GetAccessTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
        
        var response = await SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();

            // if its forbidden, the message is empty
            if (string.IsNullOrEmpty(errorResponse)) errorResponse = "This resource is not accessible";

            throw new HttpRequestException(errorResponse);
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(jsonResponse)!;
    }

    public async Task<T> GetAsync<T>(string uri, object id)
    {
        return await GetAsync<T>(uri.ReplaceInUrl(id));
    }
    
    public async Task DeleteAsync(string uri, object id)
    {
        var accessToken = await GetAccessTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Delete, uri.ReplaceInUrl(id));

        if (!string.IsNullOrWhiteSpace(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorResponse = await response.Content.ReadAsStringAsync();

            // if its forbidden, the message is empty
            if (string.IsNullOrEmpty(errorResponse)) errorResponse = "This resource is not accessible";

            throw new HttpRequestException(errorResponse);
        }
    }
}