using Microsoft.EntityFrameworkCore; 
using Backend.Models; 

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Peminjaman> Peminjamans { get; set; }
        public DbSet<Ruangan> Ruangans { get; set; }
    }
}