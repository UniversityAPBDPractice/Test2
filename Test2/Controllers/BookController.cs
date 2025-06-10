using Microsoft.AspNetCore.Mvc;
using Test2.DTOs;
using Test2.Models;
using Test2.Services.Abstractions;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetBooks([FromQuery] DateTime? releasedByDate)
    {
        try
        {
            var result = await _service.GetBooksAsync(releasedByDate);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddBook([FromBody] AddBookRequest request)
    {
        try
        {
            await _service.AddBookAsync(request);
            return Ok("Book added successfully.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}