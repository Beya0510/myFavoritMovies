using System.Text.Json;
using Microsoft.JSInterop;

namespace Favies.Services
{
    public class AuthService
    {
        private const string AuthKey = "auth_user"; // Clé de stockage pour l'utilisateur connecté
        private const string UsersKey = "registered_users"; // Clé de stockage pour les utilisateurs inscrits
        private readonly IJSRuntime _jsRuntime;

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        
        /// Récupère l'utilisateur actuellement connecté.
       
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
    }

    public class User
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
