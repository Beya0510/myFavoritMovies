using System.Collections.Generic;
using System.Threading.Tasks;
using Favies.Models; // Ajouter cette ligne

namespace Favies.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(string query);
        Task<Movie> GetMovieDetailsAsync(string id);
    }
}