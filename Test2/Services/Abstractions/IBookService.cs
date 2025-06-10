using Test2.Models;
using Test2.DTOs;
namespace Test2.Services.Abstractions;

public interface IBookService
{
    Task<IEnumerable<GetBookResponse>> GetBooksAsync(DateTime? releasedByDate);
    Task<int> AddBookAsync(AddBookRequest request);
}