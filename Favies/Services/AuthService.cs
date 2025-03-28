using System.Text.Json;
using Microsoft.JSInterop;
using Favies.Models;
namespace Favies.Services
{
    public class AuthService
    {
        private const string AuthKey = "auth_user"; // Clé pour l'utilisateur connecté
        private const string UsersKey = "registered_users"; // Clé pour les utilisateurs inscrits
        private readonly IJSRuntime _jsRuntime;

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        /// <summary>
        /// Récupère l'utilisateur actuellement connecté.
        /// </summary>
        public async Task<User?> GetCurrentUserAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AuthKey);
            return string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<User>(json);
        }

        /// <summary>
        /// Vérifie les identifiants et connecte l'utilisateur.
        /// </summary>
        public async Task<bool> LoginAsync(string email, string password)
        {
            var users = await GetRegisteredUsersAsync();
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                var json = JsonSerializer.Serialize(user);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthKey, json);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Inscrit un nouvel utilisateur et le stocke dans `localStorage`.
        /// </summary>
        public async Task<bool> RegisterAsync(string email, string password)
        {
            var users = await GetRegisteredUsersAsync();

            // Vérifie si l'email est déjà utilisé
            if (users.Any(u => u.Email == email))
            {
                return false;
            }

            var newUser = new User { Email = email, Password = password };
            users.Add(newUser);

            // Sauvegarde la liste des utilisateurs
            var jsonUsers = JsonSerializer.Serialize(users);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UsersKey, jsonUsers);

            // Connecte automatiquement l'utilisateur après inscription
            await LoginAsync(email, password);

            return true;
        }

        /// <summary>
        /// Déconnecte l'utilisateur.
        /// </summary>
        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthKey);
        }

        /// <summary>
        /// Récupère la liste des utilisateurs enregistrés.
        /// </summary>
        private async Task<List<User>> GetRegisteredUsersAsync()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UsersKey);
            return string.IsNullOrEmpty(json) ? new List<User>() : JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        #region Gestion des favoris par utilisateur

        // Récupère les favoris de l'utilisateur connecté
        public async Task<List<Movie>> GetFavoritesAsync(User user)
        {
            var key = $"favorites_{user.Email}"; // La clé unique par utilisateur
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            return string.IsNullOrEmpty(json) ? new List<Movie>() : JsonSerializer.Deserialize<List<Movie>>(json);
        }

        // Sauvegarde les favoris de l'utilisateur connecté
        public async Task SaveFavoritesAsync(User user, List<Movie> favorites)
        {
            var key = $"favorites_{user.Email}"; // La clé unique par utilisateur
            var json = JsonSerializer.Serialize(favorites);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        #endregion
    }

    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    /*public class Movie
    {
        public object imdbID;
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
    }*/
}
