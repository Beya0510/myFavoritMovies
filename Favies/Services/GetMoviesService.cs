using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Favies.Services
{

    public class GetMoviesService
    {
        private readonly HttpClient _httpClient;

        public GetMoviesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        string url = "https://www.omdbapi.com/";
        public async Task<T?> GetMoviesAsync<T>(string url) where T : class
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseContent);
        }
    }
}  