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

    public async Task<IEnumerable<GetBookResponse>> GetBooksAsync(DateTime? releasedByDate)
    {
        var query = _context.Books
            .Include(b => b.PublishingHouse)
            .Include(b => b.BookGenres)
            .ThenInclude(bg => bg.Genre)
            .Include(b => b.BookAuthors)
            .ThenInclude(ba => ba.Author)
            .AsQueryable();

        if (releasedByDate.HasValue)
            query = query.Where(b => b.ReleaseDate > releasedByDate.Value);

        var books = await query
            .OrderByDescending(b => b.ReleaseDate)
            .ThenBy(b => b.PublishingHouse.Name)
            .Select(b => new GetBookResponse
            {
                BookName = b.Name,
                ReleaseDate = b.ReleaseDate,
                PublishingHouse = new PublishingHouseDto
                {
                    Name = b.PublishingHouse.Name,
                    Country = b.PublishingHouse.Country,
                    City = b.PublishingHouse.City
                },
                Genres = b.BookGenres.Select(bg => bg.Genre.Name).ToList(),
                Authors = b.BookAuthors.Select(ba => new AuthorDto
                {
                    FirstName = ba.Author.FirstName,
                    LastName = ba.Author.LastName
                }).ToList()
            })
            .ToListAsync();

        return books;
    }

    public async Task<int> AddBookAsync(AddBookRequest request)
    {
        var publishingHouse = await _context.PublishingHouses
            .FirstOrDefaultAsync(ph => ph.IdPublishingHouse == request.PublishingHouseId);

        if (publishingHouse == null)
        {
            publishingHouse = new PublishingHouse
            {
                Name = request.PublishingHouseName,
                Country = request.Country,
                City = request.City
            };
            _context.PublishingHouses.Add(publishingHouse);
            await _context.SaveChangesAsync(); // ensure ID is generated
        }

        var book = new Book
        {
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            IdPublishingHouse = publishingHouse.IdPublishingHouse
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync(); // Save first to generate IdBook

        // Add BookAuthors
        book.BookAuthors = request.AuthorIds.Select(authorId => new BookAuthor
        {
            IdBook = book.IdBook,
            IdAuthor = authorId
        }).ToList();

        // Add BookGenres
        book.BookGenres = request.GenreIds.Select(genreId => new BookGenre
        {
            IdBook = book.IdBook,
            IdGenre = genreId
        }).ToList();

        await _context.SaveChangesAsync();

        return book.IdBook;
    }
}