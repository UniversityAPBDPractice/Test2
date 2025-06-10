using System.Collections;

namespace Test2.Models;

public class Book
{
    public int IdBook { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int IdPublishingHouse { get; set; }
    public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    public virtual ICollection<BookGenre> BookGenres { get; set; }
}