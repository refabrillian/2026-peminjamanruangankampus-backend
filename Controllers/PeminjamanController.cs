using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeminjamanController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PeminjamanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class StatusUpdateDto
        {   
            public string NewStatus { get; set; } = string.Empty;
        }

        [HttpPatch("{id}/status")]
public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateDto request) // Pakai DTO di sini
{
    var peminjaman = await _context.Peminjamans.FindAsync(id);
    if (peminjaman == null) return NotFound();

    // Ambil data dari request.NewStatus
    peminjaman.Status = request.NewStatus; 
    await _context.SaveChangesAsync();

    return Ok(peminjaman);
}

        // 1. READ: Mendapatkan semua data peminjaman yang tidak dihapus (Soft Delete)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peminjaman>>> GetPeminjamans()
        {
            // Hanya mengambil data yang IsDeleted-nya false
            return await _context.Peminjamans
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        // 2. CREATE: Menambah data peminjaman baru
        [HttpPost]
        public async Task<ActionResult<Peminjaman>> PostPeminjaman(Peminjaman peminjaman)
        {
            // Validasi input otomatis dilakukan oleh [ApiController]
            _context.Peminjamans.Add(peminjaman);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPeminjamans), new { id = peminjaman.Id }, peminjaman);
        }

        // 3. UPDATE: Mengubah data peminjaman (misal: mengubah status)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeminjaman(int id, Peminjaman peminjaman)
        {
            if (id != peminjaman.Id) return BadRequest();

            _context.Entry(peminjaman).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!_context.Peminjamans.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

// 4. DELETE: Menghapus data secara halus (Soft Delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeminjaman(int id)
        {
            var peminjaman = await _context.Peminjamans.FindAsync(id);
            if (peminjaman == null) return NotFound();

    // Logika Soft Delete: hanya mengubah flag IsDeleted menjadi true
            peminjaman.IsDeleted = true;
            peminjaman.DeletedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
    }
}