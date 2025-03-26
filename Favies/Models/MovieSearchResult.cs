using System.Text.Json.Serialization;

namespace Favies.Models
{
    public class MovieSearchResult
    {
        [JsonPropertyName("Search")]
        public List<Movie> Movies { get; set; } = new List<Movie>();

        [JsonPropertyName("totalResults")]
        public string TotalResults { get; set; } = "0";

        [JsonPropertyName("Response")]
        public string Response { get; set; } = "False";
    }
}