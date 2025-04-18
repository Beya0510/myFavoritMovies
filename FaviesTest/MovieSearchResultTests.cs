namespace FaviesTest;
using System.Linq;
using Favies.Models;
using Xunit;


public class MovieSearchResultTests
{
    [Fact]
    public void RetriveFirstOrDefaultMovie()
    {
        // Arrange
        var searchResults = new MovieSearchResult
        {   Movies = new List<Movie>
            {
                new Movie { Title = "Inception", Year = "2010", Genre = "Sci-Fi", Director = "Christopher Nolan", Plot = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", Poster = "https://example.com/inception.jpg", imdbID = "tt1375666" },
                new Movie { Title = "The Matrix", Year = "1999", Genre = "Action", Director = "Lana Wachowski, Lilly Wachowski", Plot = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Poster = "https://example.com/matrix.jpg", imdbID = "tt0133093" }
            },
            TotalResults = "1",
            Response = "True"
        };

        // Act
        var result = searchResults.Movies.FirstOrDefault();

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Inception", result.Title);
    }
}