using Microsoft.AspNetCore.Mvc;
using Test2.Models;

public class StudentsController : ControllerBase
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        [HttpGet("actors")]
        public async Task<IActionResult> GetActors([FromQuery] string? name, [FromQuery] string? surname)
        {
            try
            {
                var result = await _service.GetActorsAsync(name, surname);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMovie([FromBody] AddMovieRequest request)
        {
            try
            {
                await _service.AddMovieAsync(request);
                return Ok("Movie added successfully.");
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
}