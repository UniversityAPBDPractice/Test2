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
                AuthorId = b.IdAuthor,
                Authors = a.ActorMovies.Select(am => new MovieDto
                {
                    Name = am.Movie.Name,
                    ReleaseDate = am.Movie.ReleaseDate,
                    AgeRating = am.Movie.AgeRating.Name,
                    CharacterName = am.CharacterName
                }).ToList()
            }).ToListAsync();
    }

    public Task<int> AddBookAsync(AddBookRequest request)
    {
        throw new NotImplementedException();
    }
}