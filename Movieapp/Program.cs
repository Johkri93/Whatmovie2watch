using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        MovieLibrary movieLibrary = new MovieLibrary();
        
        // Skapa en instans av MovieApiService
        MovieApiService movieApiService = new MovieApiService();

        // Hämta och visa alla tillgängliga genrer
        Console.WriteLine("Hämtar tillgängliga genrer...");
        await movieApiService.LoadGenresAsync();

        // Fråga användaren om en eller flera genrer att söka efter
        Console.WriteLine("\nAnge ID eller namn för en eller flera genrer (separera med komma):");
        string genreInput = Console.ReadLine();

        // Hämta genre-ID:n baserat på användarens input
        List<int> genreIds = movieApiService.GetGenreIdsFromInput(genreInput);

        if (genreIds.Count > 0)
        {
            // Hämta en slumpmässig film från de valda genrerna
            Console.WriteLine($"\nHämtar en slumpmässig film från genrer: {string.Join(", ", genreIds)}");
            await movieApiService.GetRandomMovieByGenresAsync(genreIds);
        }
        else
        {
            Console.WriteLine("Inga giltiga genrer angavs.");
        }
    }
}