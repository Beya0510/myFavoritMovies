using System.Collections.Generic;

namespace Favies.Services
{
    public class FavoritesService
    {
        private List<string> _favorites = new List<string>();
        public IReadOnlyList<string> Favorites => _favorites;
        
        public List<string> GetFavorites()
        {
            return _favorites;
        }
      //  public void AddFavorite(string favorite)
      //  {
      //      _favorites.Add(favorite);
      //  }
        
        public void AddFavorite(string item)
        {
            if (!_favorites.Contains(item))
            {
                _favorites.Add(item);
            }
        }
        //public void RemoveFavorite(string favorite)
        //{
        //    _favorites.Remove(favorite);
        //}
        
        public void RemoveFavorite(string item)
        {
            if (_favorites.Contains(item))
            {
                _favorites.Remove(item);
            }
        }
        
    }
}
