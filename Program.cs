using Backend.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowReact");

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers();

// --- BAGIAN SEEDER DATA ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    
    // Memastikan database dan tabel sudah terbentuk
    context.Database.EnsureCreated();

    // Cek apakah tabel Peminjamans masih kosong
    if (!context.Peminjamans.Any())
    {
        context.Peminjamans.AddRange(
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Refa Brillian", 
                NamaRuangan = "Lab ICT", 
                TanggalMulai = DateTime.Now, 
                TanggalSelesai = DateTime.Now.AddHours(2), 
                Status = "Disetujui",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Andi Saputra", 
                NamaRuangan = "Aula Utama", 
                TanggalMulai = DateTime.Now.AddDays(1), 
                TanggalSelesai = DateTime.Now.AddDays(1).AddHours(3), 
                Status = "Menunggu",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Siti Aminah", 
                NamaRuangan = "Ruang Rapat A", 
                TanggalMulai = DateTime.Now.AddDays(2), 
                TanggalSelesai = DateTime.Now.AddDays(2).AddHours(1), 
                Status = "Menunggu",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Budi Doremi", 
                NamaRuangan = "Lab RPL", 
                TanggalMulai = DateTime.Now.AddDays(3), 
                TanggalSelesai = DateTime.Now.AddDays(3).AddHours(4), 
                Status = "Ditolak",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Dewi Lestari", 
                NamaRuangan = "Lab Jaringan", 
                TanggalMulai = DateTime.Now.AddDays(4), 
                TanggalSelesai = DateTime.Now.AddDays(4).AddHours(2), 
                Status = "Disetujui",
                IsDeleted = false 
            }
        );
        // Simpan semua data ke database
        context.SaveChanges();
    }
}

app.Run();
