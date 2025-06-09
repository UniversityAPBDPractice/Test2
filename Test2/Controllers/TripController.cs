using Microsoft.AspNetCore.Mvc;
using Test2.Models;

public class StudentsController : ControllerBase
{
    private readonly SchoolContext _context;
    public StudentsController(SchoolContext context)
    {
        _context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
    {
        return await _context.Students.ToListAsync();
    }
}