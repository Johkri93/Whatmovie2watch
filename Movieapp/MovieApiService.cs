using System;
using System.Collections.Generic;
using System.IO;  // För att läsa appsettings.json
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class MovieApiService
{
    private readonly string ApiKey;

    private static readonly string BaseUrl = "https://api.themoviedb.org/3";
    private static Dictionary<string, int> genres = new Dictionary<string, int>();

    public MovieApiService()
    {
        ApiKey = GetAPIKey();  // Läs API-nyckeln från appsettings.json
    }

    // Hämta API-nyckeln från appsetting.json
    private static string GetAPIKey()
    {
        var configFilePath = "appsetting.json";
        if (File.Exists(configFilePath))
        {
            var configJson = File.ReadAllText(configFilePath);
            var config = JObject.Parse(configJson);
            var apiKey = config["ApiKey"]?.ToString();

            if (string.IsNullOrEmpty(apiKey))
            {
                Console.WriteLine("Ingen API-nyckel hittades i appsettings.json.");
                return "";
            }
            else
            {
                Console.WriteLine("API-nyckel laddad korrekt.");
                return apiKey;
            }
        }
        else
        {
            Console.WriteLine("appsettings.json hittades inte.");
            return "";
        }
    }

    public async Task LoadGenresAsync()
    {
        if (string.IsNullOrEmpty(ApiKey))
        {
            Console.WriteLine("API-nyckeln är ogiltig eller saknas.");
            return;
        }

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = $"{BaseUrl}/genre/movie/list?api_key={ApiKey}&language=en-US";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(result);

                    Console.WriteLine("Tillgängliga genrer:");
                    foreach (var genre in json["genres"])
                    {
                        string genreName = genre["name"].ToString().ToLower();
                        int genreId = (int)genre["id"];
                        genres[genreName] = genreId;

                        Console.WriteLine($"{genreId}: {genre["name"]}");
                    }
                }
                else
                {
                    Console.WriteLine($"Fel: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
    }

    public async Task GetRandomMovieByGenresAsync(List<int> genreIds)
    {
        if (string.IsNullOrEmpty(ApiKey))
        {
            Console.WriteLine("API-nyckeln är ogiltig eller saknas.");
            return;
        }

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string genreIdString = string.Join(",", genreIds);
                string url = $"{BaseUrl}/discover/movie?api_key={ApiKey}&with_genres={genreIdString}";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(result);

                    var movies = json["results"];
                    if (movies.Count() > 0)
                    {
                        Random rand = new Random();
                        int randomIndex = rand.Next(movies.Count());
                        var randomMovie = movies[randomIndex];

                        string title = randomMovie["title"].ToString();
                        string overview = randomMovie["overview"].ToString();
                        string releaseDate = randomMovie["release_date"].ToString();

                        Console.WriteLine($"Titel: {title}");
                        Console.WriteLine($"Handling: {overview}");
                        Console.WriteLine($"Utgivningsdatum: {releaseDate}");
                    }
                    else
                    {
                        Console.WriteLine("Inga filmer hittades för de här genrerna.");
                    }
                }
                else
                {
                    Console.WriteLine($"Fel: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel uppstod: {ex.Message}");
            }
        }
    }

    public List<int> GetGenreIdsFromInput(string input)
    {
        var genreIds = new List<int>();
        var genreInputs = input.ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var genreInput in genreInputs)
        {
            var trimmedInput = genreInput.Trim();

            if (int.TryParse(trimmedInput, out int genreId))
            {
                if (genres.Values.Contains(genreId))
                {
                    genreIds.Add(genreId);
                }
                else
                {
                    Console.WriteLine($"Fel: Genre-ID {genreId} finns inte.");
                }
            }
            else if (genres.ContainsKey(trimmedInput))
            {
                genreIds.Add(genres[trimmedInput]);
            }
            else
            {
                Console.WriteLine($"Fel: Genre-namnet '{trimmedInput}' finns inte.");
            }
        }

        return genreIds;
    }
}