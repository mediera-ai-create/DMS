using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

public class LocationService : ILocationService
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public LocationService(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<IEnumerable<string>> GetCountriesAsync()
    {
        // Example using restcountries
        var url = "https://restcountries.com/v3.1/all";
        var arr = await _http.GetFromJsonAsync<List<dynamic>>(url);
        return arr?.Select(x => (string)x.name.common).OrderBy(n => n).ToList() ?? new List<string>();
    }

    public async Task<IEnumerable<string>> GetStatesAsync(string country)
    {
        // Example provider: countriesnow API (POST)
        var url = "https://countriesnow.space/api/v0.1/countries/states";
        var payload = new { country = country };
        var resp = await _http.PostAsJsonAsync(url, payload);
        if (!resp.IsSuccessStatusCode) return new List<string>();
        var doc = await resp.Content.ReadFromJsonAsync<dynamic>();
        try
        {
            // Fix: Check for null before dereferencing
            if (doc?.data?.states is IEnumerable<object> statesObj)
            {
                var states = statesObj.Select(s => (string)((dynamic)s).name).ToList();
                return states;
            }
            return new List<string>();
        }
        catch
        {
            return new List<string>();
        }
    }
}
