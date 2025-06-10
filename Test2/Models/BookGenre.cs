namespace Test2.Models;

public class BookGenre
{
    public int IdBook { get; set; }
    public Book Book { get; set; }
    public int IdGenre { get; set; }
    public Genre Genre { get; set; }
}