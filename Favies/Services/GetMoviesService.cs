using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Favies.Models;

namespace Favies.Services
{
    public class GetMoviesService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "d0c9b0d0"; // Remplacez par votre clé API valide
        private const string BaseUrl = "https://www.omdbapi.com/";

        public GetMoviesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string searchQuery, int page)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}?apikey={ApiKey}&s={searchQuery}&page={page}");
            if (!response.IsSuccessStatusCode)
                return new List<Movie>();

            var responseContent = await response.Content.ReadAsStringAsync();
            var searchResult = JsonSerializer.Deserialize<MovieSearchResult>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return searchResult?.Movies ?? new List<Movie>();
        }
    }
}