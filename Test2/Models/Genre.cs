namespace Test2.Models;

public class Genre
{
    public int IdGenre { get; set; }
    public string Name { get; set; }
    public virtual ICollection<BookGenre> BookGenres { get; set; }
}