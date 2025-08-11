public interface ILocationService
{
    Task<IEnumerable<string>> GetCountriesAsync();
    Task<IEnumerable<string>> GetStatesAsync(string country);
}
