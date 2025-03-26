using System.Collections.Generic;
using System.Threading.Tasks;
using Favies.Models; 

namespace Favies.Services
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(string titre);
        Task<Movie> GetMovieDetailsAsync(string id);
    }
}