# Favies

Favies est une application web construite avec Blazor qui permet aux utilisateurs de gérer leurs films favoris. Elle inclut des fonctionnalités pour l'authentification des utilisateurs, la recherche de films et la gestion d'une liste de favoris.

## Fonctionnalités

-   Inscription et connexion des utilisateurs
-   Recherche de films en utilisant l'API OMDB
-   Ajout de films à une liste de favoris personnelle
-   Suppression de films de la liste des favoris

## Technologies Utilisées

-   **Blazor**: Un framework web pour construire des interfaces utilisateur web interactives en utilisant C#.
-   **C#**: Le langage de programmation utilisé pour l'application Blazor.
-   **OMDB API**: Une API de base de données de films ouverte utilisée pour la recherche d'informations sur les films. (https://www.omdbapi.com)
-   **xUnit**: Un framework de test pour .NET.
-   **Moq**: Une librairie de mocking pour .NET.

## Instructions d'Installation

1.  **Prérequis :**
    -   [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0)
    -   Un navigateur web moderne

2.  **Cloner le dépôt :**

    ```bash
    git clone https://github.com/Beya0510/myFavoritMovies.git
    cd Favies
    ```

3.  **Restaurer les dépendances :**

    ```bash
    dotnet restore
    ```

4.  **Compiler le projet :**

    ```bash
    dotnet build
    ```

5.  **Exécuter l'application :**

    ```bash
    dotnet run
    ```

    L'application démarrera et vous pourrez y accéder via votre navigateur web à l'URL spécifiée (généralement `http://localhost:5000`).