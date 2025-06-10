using Test2.Models;
using Test2.Services.Abstractions;

namespace Test2.Services;

public class MovieService : IMovieService
{
    private readonly AppDbContext _context;

    public MovieService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ActorDto>> GetActorsAsync(string? name, string? surname)
    {
        var query = _context.Actors
            .Include(a => a.ActorMovies)
                .ThenInclude(am => am.Movie)
                    .ThenInclude(m => m.AgeRating)
            .AsQueryable();

        if (!string.IsNullOrEmpty(name))
            query = query.Where(a => a.Name.Contains(name));

        if (!string.IsNullOrEmpty(surname))
            query = query.Where(a => a.Surname.Contains(surname));

        return await query
            .OrderBy(a => a.Name)
            .Select(a => new ActorDto
            {
                IdActor = a.IdActor,
                Name = a.Name,
                Surname = a.Surname,
                Nickname = a.Nickname,
                Movies = a.ActorMovies.Select(am => new MovieDto
                {
                    Name = am.Movie.Name,
                    ReleaseDate = am.Movie.ReleaseDate,
                    AgeRating = am.Movie.AgeRating.Name,
                    CharacterName = am.CharacterName
                }).ToList()
            }).ToListAsync();
    }

    public async Task AddMovieAsync(AddMovieRequest request)
    {
        if (!await _context.AgeRatings.AnyAsync(ar => ar.IdRating == request.IdAgeRating))
            throw new ArgumentException("Invalid age rating.");

        var actorIds = request.Actors.Select(a => a.IdActor);
        var existingActors = await _context.Actors.Where(a => actorIds.Contains(a.IdActor)).ToListAsync();

        if (existingActors.Count != request.Actors.Count)
            throw new ArgumentException("One or more actor IDs are invalid.");

        var movie = new Movie
        {
            Name = request.Name,
            ReleaseDate = request.ReleaseDate,
            IdAgeRating = request.IdAgeRating,
            ActorMovies = request.Actors.Select(a => new ActorMovie
            {
                IdActor = a.IdActor,
                CharacterName = a.CharacterName
            }).ToList()
        };

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }
}