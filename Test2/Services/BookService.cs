using Microsoft.EntityFrameworkCore;
using Test2.DTOs;
using Test2.Infrastructure;
using Test2.Models;
using Test2.Services.Abstractions;

namespace Test2.Services;

public class BookService : IBookService
{
    private readonly BookContext _context;
    public BookService(BookContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Book>> GetBooksAsync(DateTime? releasedByDate)
    {
        var query = _context.Books
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.IdAuthor).ThenInclude(ba => ba.Author)
            .AsQueryable();

        if (!releasedByDate.HasValue)
            query = query.Where(a => a.ReleaseDate > releasedByDate);

        return await query
            .OrderDescending(b => b.ReleaseDate).OrderBy(b => b.PublishingHouse.Name)
            .Select(b => new GetBookResponse
            {
                BookName = b.Name,
                Genre = b.Genre,
                Authors = a.ActorMovies.Select(am => new Author
                {
                    Name = am.Movie.Name,
                    Age = a.Age,
                }).ToList()
            }).ToListAsync();
    }

    public async Task<int> AddBookAsync(AddBookRequest request)
    {
        throw new NotImplementedException();
    }
}