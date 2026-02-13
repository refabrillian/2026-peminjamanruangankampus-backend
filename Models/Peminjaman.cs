using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Peminjaman
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string NamaPeminjam { get; set; } = string.Empty;

        [Required]
        public string NamaRuangan { get; set; } = string.Empty;

        [Required]
        public DateTime TanggalMulai { get; set; }

        [Required]
        public DateTime TanggalSelesai { get; set; }

        [Required]
        public string Status { get; set; } = "Menunggu";

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
    }
}