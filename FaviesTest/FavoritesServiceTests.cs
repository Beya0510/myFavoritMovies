namespace FaviesTests;
using Favies.Services;

using Favies.Models;
using Microsoft.JSInterop;
using Moq;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class FavoriteServiceTests
{
    private Mock<IJSRuntime> _jsRuntimeMock;
    //instance de la classe FavoriteService
    private FavoritesService _favoriteService;
    private List<Movie> _favoriteMovies;
   
    

    // Constructeur pour initialiser les dépendances
    public FavoriteServiceTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _favoriteService = new FavoritesService(_jsRuntimeMock.Object);
    }

    [Fact]
    public async Task AddFavorite_ShouldAddMovie_WhenNotAlreadyFavorite()
    {
        // Arrange
        var user = new User { Email = "test@example.com" };
        var movie = new Movie { imdbID = "tt1234567", Title = "Test Movie" };
        var key = $"favorites_{user.Email}";
        var expectedValue = JsonSerializer.Serialize(new List<Movie> { movie });

        
        _jsRuntimeMock.Setup(js => js.InvokeAsync<object>("localStorage.setItem", It.IsAny<object[]>()))
            .Returns(new ValueTask<object>(Task.FromResult<object>(null))); // Simule un appel réussi


        // Act
        await _favoriteService.AddFavorite(movie);

        // Assert
        _jsRuntimeMock.Verify(js => js.InvokeAsync<object>("localStorage.setItem", It.Is<object[]>(args =>
            args[0].ToString() == key && args[1].ToString() == expectedValue)), Times.Once);
    }
}