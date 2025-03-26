
using System.Text.Json;
using Microsoft.JSInterop;
using Favies.Models;
public class FavoritesService
{
    private readonly IJSRuntime jsRuntime;
    private const string StorageKey = "favorite_movies";

    public FavoritesService(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    // Ajouter un favori et l'enregistrer dans le localStorage
    public async Task AddFavorites(string movieTitle)
    {
        var favorites = await GetFavoritesAsync();
        if (!favorites.Contains(movieTitle))
        {
            favorites.Add(movieTitle);
            await SaveFavoritesAsync(favorites);
        }
    }

    // Supprimer un favori et mettre à jour le localStorage
    public async Task RemoveFavorites(string movieTitle)
    {
        var favorites = await GetFavoritesAsync();
        if (favorites.Contains(movieTitle))
        {
            favorites.Remove(movieTitle);
            await SaveFavoritesAsync(favorites);
        }
    }

    // Récupérer les favoris depuis le localStorage
    public async Task<List<string>> GetFavoritesAsync()
    {
        var json = await jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey);
        return json != null ? JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>() : new List<string>();
    }

    // Sauvegarder les favoris dans le localStorage
    private async Task SaveFavoritesAsync(List<string> favorites)
    {
        var json = JsonSerializer.Serialize(favorites);
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json);
    }
}