public class Movie
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }

    public Movie(string title, string director, int year, string genre)
    {
        Title = title;
        Director = director;
        Year = year;
        Genre = genre;
    }

    public override string ToString()
    {
        return $"{Title} directed by {Director}, released in {Year}, Genre: {Genre}";
    }
}