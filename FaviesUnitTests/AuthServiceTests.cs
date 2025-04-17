using System.Text.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Moq;
using Xunit;
using Favies.Models;
using Favies.Services;


namespace FaviesTest;

public class AuthServiceTests
{
    private readonly Mock<IJSRuntime> _jsRuntimeMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _jsRuntimeMock = new Mock<IJSRuntime>();
        _authService = new AuthService(_jsRuntimeMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ShouldReturnTrue_WhenCredentialsAreValid()
    {
        var user = new User { Email = "test@example.com", Password = "password" };
        _jsRuntimeMock.Setup(js => js.InvokeAsync<string>("localStorage.getItem", It.IsAny<object[]>() ))
            .ReturnsAsync(JsonSerializer.Serialize(new List<User> { user }));
        
        var result = await _authService.LoginAsync("test@example.com", "password");
        
        Assert.True(result);
    }
}