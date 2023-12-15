using Newtonsoft.Json;
using System.Text;

namespace InjectionMachineModule.Infrastructure.Communication;

public class RestClient
{
    private readonly HttpClient _httpClient;

    public RestClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task PostAsync<T>(string url, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync(url, content);
        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException($"{result.StatusCode}: {message}");
        }
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
        }
        return default;
    }

    public async Task PutAsync<T>(string url, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _httpClient.PutAsync(url, content);
        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message);
        }
    }

    public async Task PatchAsync<T>(string url, T data)
    {
        var json = JsonConvert.SerializeObject(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var result = await _httpClient.PatchAsync(url, content);
        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message);
        }
    }

    public async Task DeleteAsync(string url)
    {
        var result = await _httpClient.DeleteAsync(url);
        try
        {
            result.EnsureSuccessStatusCode();
        }
        catch
        {
            var message = await result.Content.ReadAsStringAsync();
            throw new HttpRequestException(message);
        }
    }
}