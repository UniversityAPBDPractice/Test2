namespace Test2.Models;

public class BookAuthor
{
    public int IdBook { get; set; }
    public Book Book { get; set; }
    public int IdAuthor { get; set; }
    public Author Author { get; set; }
}