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
    **⚠️ Veuillez utiliser la branche `develop` pour le développement et les tests.**

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
------

# FaviesTest

## Description

FaviesTest est un projet de test pour l'application Favies. Il contient des tests unitaires pour les différents services de l'application, tels que `GetMoviesService`.

## Tests

-   `GetMoviesServiceTests.cs`: Contient les tests unitaires pour la classe `GetMoviesService`.
-   `AuthServiceTests.cs`: Contient les tests unitaires pour la classe `AuthService`.
-   `MovieSearchResultTests.cs`: Contient les tests unitaires pour la classe `MovieSearchResult`.

## Comment exécuter les tests

Pour exécuter les tests, vous pouvez utiliser l'IDE JetBrains Rider ou la ligne de commande avec la commande `dotnet test`.

### Prérequis

-   [.NET SDK](https://dotnet.microsoft.com/download) (version 9.0)

### Étapes

1.  Ouvrez le projet dans JetBrains Rider ou un autre IDE compatible avec .NET.
2.  Exécutez les tests via l'interface de l'IDE ou en utilisant la commande `dotnet test` dans le répertoire du projet.

## Dépendances

Le projet utilise les dépendances suivantes :

-   xUnit
-   Moq
-   Microsoft.NET.Test.Sdk

Ces dépendances sont définies dans le fichier `.csproj` du projet.