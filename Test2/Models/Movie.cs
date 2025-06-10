namespace Test2.Models;

public class Movie
{
    public int IdMovie { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }

    public int IdAgeRating { get; set; }
    public AgeRating AgeRating { get; set; }

    public ICollection<ActorMovie> ActorMovies { get; set; }
}