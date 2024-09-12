using System;
using System.Collections.Generic;

public class MovieLibrary
{
    private List<Movie> movies;

    public MovieLibrary()
    {
        movies = new List<Movie>();
    }

    public void AddMovie(Movie movie)
    {
        movies.Add(movie);
        Console.WriteLine($"{movie.Title} has been added.");
    }

    public void RemoveMovie(string title)
    {
        Movie movieToRemove = movies.Find(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (movieToRemove != null)
        {
            movies.Remove(movieToRemove);
            Console.WriteLine($"{title} has been removed.");
        }
        else
        {
            Console.WriteLine($"Movie titled '{title}' not found.");
        }
    }

    public void ListMovies()
    {
        if (movies.Count == 0)
        {
            Console.WriteLine("No movies in the library.");
        }
        else
        {
            Console.WriteLine("Movies in the library:");
            foreach (var movie in movies)
            {
                Console.WriteLine(movie.ToString());
            }
        }
    }

    public Movie SearchMovie(string title)
    {
        Movie movie = movies.Find(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        if (movie != null)
        {
            Console.WriteLine($"Found: {movie.ToString()}");
            return movie;
        }
        else
        {
            Console.WriteLine($"Movie titled '{title}' not found.");
            return null;
        }
    }

    public void RecommendMovie()
    {
        if (movies.Count > 0)
        {
            Random rand = new Random();
            int randomIndex = rand.Next(movies.Count);
            Movie randomMovie = movies[randomIndex];
            Console.WriteLine($"We recommend you to watch: {randomMovie.ToString()}");
        }
        else
        {
            Console.WriteLine("No movies available for recommendation.");
        }
    }
}