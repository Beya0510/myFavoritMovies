using System.Collections.Generic;  // Importation de l'espace de noms pour utiliser les collections génériques

namespace Favies.Services
{
    public class FavoritesService
    {
        private List<string> _favorites = new List<string>();  // Liste privée pour stocker les favoris

        // Propriété en lecture seule pour accéder à la liste des favoris
        public IReadOnlyList<string> Favorites => _favorites;

        // Méthode pour obtenir la liste des favoris
        public List<string> GetFavorites()
        {
            return _favorites;  // Retourne la liste des favoris
        }

        // Méthode pour ajouter un nouvel élément aux favoris
        public void AddFavorites(string item)
        {
            if (!_favorites.Contains(item))  // Vérifie si l'élément n'est pas déjà dans la liste
            {
                _favorites.Add(item);  // Ajoute l'élément à la liste des favoris
            }
        }

        // Méthode pour supprimer un élément des favoris
        public void RemoveFavorites(string item)
        {
            if (_favorites.Contains(item))  // Vérifie si l'élément est dans la liste des favoris
            {
                _favorites.Remove(item);  // Supprime l'élément de la liste des favoris
            }
        }

        // Méthode pour modifier un élément dans les favoris
        public void EditFavorites(string oldItem, string newItem)
        {
            var index = _favorites.IndexOf(oldItem);  // Trouve l'index de l'élément à modifier
            if (index != -1)  // Vérifie si l'élément existe dans la liste
            {
                _favorites[index] = newItem;  // Remplace l'ancien élément par le nouveau
            }
        }
    }
}