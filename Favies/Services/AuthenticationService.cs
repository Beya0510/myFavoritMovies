using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Favies.Services
{
    public class AuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public AuthenticationService(HttpClient httpClient, IAccessTokenProvider accessTokenProvider)
        {
            _httpClient = httpClient;
            _accessTokenProvider = accessTokenProvider;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var result = await _accessTokenProvider.RequestAccessToken();
            if (result.TryGetToken(out var token))
            {
                return token.Value;
            }
            return string.Empty; // Utiliser une valeur par défaut appropriée
        }

        public async Task LogInAsync()
        {
            await Task.Run(() =>
            {
                // Logique de connexion
            });
        }

        public async Task LogOutAsync()
        {
            await Task.Run(() =>
            {
                // Logique de déconnexion
            });
        }
    }
}