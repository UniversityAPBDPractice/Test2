namespace Test2.Services.Abstractions;

public interface IMovieService
{
    Task<List<ActorDto>> GetActorsAsync(string? name, string? surname);
    Task AddMovieAsync(AddMovieRequest request);
}