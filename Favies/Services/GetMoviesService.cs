using System.Net.Http;

using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Favies.Models; // Ajouter cette ligne

namespace Favies.Services
{
    public class GetMoviesService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public GetMoviesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(string query)
        {
            var response = await _httpClient.GetAsync("https://www.omdbapi.com/?apikey=d0c9b0d0Y&t=test");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Movie>>(responseContent) ?? new List<Movie>();
        }

       // public async Task<Movie> GetMovieDetailsAsync(string id)
        //{
         //   var response = await _httpClient.GetAsync($"https://www.omdbapi.com/?apikey=YOUR_API_KEY&i={id}");
         //   response.EnsureSuccessStatusCode();
         //   var responseContent = await response.Content.ReadAsStringAsync();
         //   return JsonSerializer.Deserialize<Movie>(responseContent) ?? new Movie();
       // }
    }
}