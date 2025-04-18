using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Mock;
using Xunit;
using Favies.Services;
using System.Linq;
public class GetMoviesServiceTests
{
    [Fact]
    public async Task SearchMoviesAsync_ShouldReturnMovies_WhenApiResponseIsValid()
    {
        // Arrange
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"Search\":[{\"Title\":\"Inception\",\"Year\":\"2010\"}]}")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        var getMoviesService = new GetMoviesService(httpClient);

        // Act
        var result = await getMoviesService.SearchMoviesAsync("Inception", 1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Inception", result.First().Title);
    }
}