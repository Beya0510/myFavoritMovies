using System.Collections.Generic;

namespace Favies.Services
{
    public class FavoritesService
    {
        private List<string> _favorites = new List<string>();
        public IReadOnlyList<string> Favorites => _favorites;
        
        public List<string> GetFavorites()
        {
            return favorites;
        }
        public void AddFavorite(string favorite)
        {
            _favorites.Add(favorite);
        }
        
        public void AddFavorite(string item)
        {
            if (!favorites.Contains(item))
            {
                favorites.Add(item);
            }
        }
        public void RemoveFavorite(string favorite)
        {
            _favorites.Remove(favorite);
        }
        
        public void RemoveFavorite(string item)
        {
            if (favorites.Contains(item))
            {
                favorites.Remove(item);
            }
        }
    }
}
