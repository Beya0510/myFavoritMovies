using System.Text.Json;
using Microsoft.JSInterop;

namespace Favies.Services
{
    public class AuthService
    {
        private const string AuthKey = "auth_user"; // Clé de stockage
        private readonly IJSRuntime _jsRuntime;

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AuthKey);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<User>(json);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            // Utilisateur fictif (à remplacer par une base de données)
            if (username == "admin" && password == "1234")
            {
                var user = new User { Username = username };
                var json = JsonSerializer.Serialize(user);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthKey, json);
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthKey);
        }
    }

    public class User
    {
        public string Username { get; set; } = string.Empty;
    }
}