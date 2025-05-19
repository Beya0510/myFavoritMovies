using System.Text.Json;
using Microsoft.JSInterop;
using Favies.Models;

namespace Favies.Services;

public class AuthService
{
    private const string AuthKey = "auth_user";
    private const string AdminAuthKey = "auth_admin";
    private const string UsersKey = "registered_users";
    private readonly IJSRuntime _jsRuntime;
    private User? _currentUser;
    private User? _currentAdmin;

    public event Action? AuthenticationChanged;

    public AuthService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    // Récupère l'utilisateur actuellement connecté.
    public async Task<User?> GetCurrentUserAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AuthKey);
        _currentUser = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<User>(json);
        return _currentUser;
    }

    // Récupère l'utilisateur admin actuellement connecté.
    public async Task<User?> GetCurrentAdminAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", AdminAuthKey);
        _currentAdmin = string.IsNullOrEmpty(json) ? null : JsonSerializer.Deserialize<User>(json);
        return _currentAdmin;
    }

    // Vérifie les identifiants et connecte l'utilisateur.
    public async Task<bool> LoginAsync(string email, string password)
    {
        var users = await GetRegisteredUsersAsync();
        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            var json = JsonSerializer.Serialize(user);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthKey, json);
            _currentUser = user;
            NotifyAuthenticationChanged();
            return true;
        }

        return false;
    }

    // Authentifie un utilisateur en tant qu'admin.
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        var users = await GetRegisteredUsersAsync();
        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password && u.Role == "Admin");

        if (user != null)
        {
            var json = JsonSerializer.Serialize(user);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AdminAuthKey, json);
            _currentAdmin = user;
            NotifyAuthenticationChanged();
            return user;
        }

        return null;
    }

    // Inscrit un nouvel utilisateur et le stocke dans `localStorage`.
    public async Task<bool> RegisterAsync(string email, string password)
    {
        var users = await GetRegisteredUsersAsync();

        // Vérifie si l'email est déjà utilisé
        if (users.Any(u => u.Email == email))
        {
            return false;
        }

        // Vérifie si un administrateur existe déjà
        var existingAdmin = users.FirstOrDefault(u => u.Role == "Admin");

        // Si aucun administrateur n'existe, on assigne le rôle "Admin" au premier utilisateur
        if (existingAdmin == null)
        {
            var adminUser = new User { Email = email, Password = password, Role = "Admin" };
            users.Add(adminUser);
        }
        else
        {
            // Si un administrateur existe déjà, on assigne le rôle "User" aux nouveaux utilisateurs
            var newUser = new User { Email = email, Password = password, Role = "User" };
            users.Add(newUser);
        }

        var jsonUsers = JsonSerializer.Serialize(users);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UsersKey, jsonUsers);

        return true;
    }

    // Déconnecte l'utilisateur.
    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthKey);
        _currentUser = null;
        NotifyAuthenticationChanged();
    }

    // Déconnecte l'admin.
    public async Task LogoutAdminAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AdminAuthKey);
        _currentAdmin = null;
        NotifyAuthenticationChanged();
    }

    // Récupère la liste des utilisateurs enregistrés.
    private async Task<List<User>> GetRegisteredUsersAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UsersKey);
        return string.IsNullOrEmpty(json) ? new List<User>() : JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

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

    // Supprime un utilisateur spécifique et ses favoris
    public async Task DeleteUserAsync(string email)
    {
        var users = await GetRegisteredUsersAsync();
        var userToRemove = users.FirstOrDefault(u => u.Email == email);
        if (userToRemove != null)
        {
            // Supprimer les favoris de cet utilisateur avant de le supprimer
            await DeleteUserFavoritesAsync(email);
            users.Remove(userToRemove);
            var jsonUsers = JsonSerializer.Serialize(users);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UsersKey, jsonUsers);
        }
    }

    // Supprime tous les utilisateurs et leurs favoris
    public async Task DeleteAllUsersAsync()
    {
        // Supprimer les favoris de tous les utilisateurs
        await DeleteAllFavoritesAsync();
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UsersKey);
    }

    // Supprime les favoris d'un utilisateur spécifique
    public async Task DeleteUserFavoritesAsync(string email)
    {
        var key = $"favorites_{email}";
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    // Supprime tous les favoris de tous les utilisateurs
    public async Task DeleteAllFavoritesAsync()
    {
        var users = await GetRegisteredUsersAsync();
        foreach (var user in users)
        {
            var key = $"favorites_{user.Email}";
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }
    }

    // Vérifie si un utilisateur est authentifié
    public bool IsAuthenticated()
    {
        return _currentUser != null;
    }

    private void NotifyAuthenticationChanged()
    {
        AuthenticationChanged?.Invoke();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", UsersKey);
        return string.IsNullOrEmpty(json) ? new List<User>() : JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }
}
