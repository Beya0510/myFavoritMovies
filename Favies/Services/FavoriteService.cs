using System.Collections.Generic;
using System.Text.Json;
using Microsoft.JSInterop;
using Favies.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Text.Json.Serialization;
using System;
using Favies.Services;
public class FavoritesService
{
    private readonly IJSRuntime jsRuntime;
    private const string StorageKey = "favorite_movies";
    private List<Movie> favorites = new();
    private User? currentUser;

    public FavoritesService(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }
    private async Task AddFavorite(Movie movie)
    {
        if (movie != null)
        {
            // Ajouter le film aux favoris
            favorites.Add(movie);

            // Sauvegarder les favoris de l'utilisateur
            if (currentUser is not null)
            {
                await AuthService.SaveFavoritesAsync(currentUser, favorites);
            }
        }
    }

    private async Task RemoveFavorite(Movie movie)
    {
        // Retirer le film des favoris
        favorites.Remove(movie);

        // Sauvegarder les favoris après la suppression
        if (currentUser is not null)
        {
            await AuthService.SaveFavoritesAsync(currentUser, favorites);
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