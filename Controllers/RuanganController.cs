using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class RuanganController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public RuanganController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetRuangan()
    {
        var data = await _context.Ruangans.ToListAsync();
        return Ok(data);
    }
}