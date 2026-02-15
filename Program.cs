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
    
    // Pastikan database dan tabel terbentuk
    context.Database.EnsureCreated();

    // 1. SEED DATA RUANGAN (Daftar Gedung Tetap)
    if (!context.Ruangans.Any())
    {
        context.Ruangans.AddRange(
            new Backend.Models.Ruangan { NamaRuangan = "Gedung D4" },
            new Backend.Models.Ruangan { NamaRuangan = "Gedung D3" },
            new Backend.Models.Ruangan { NamaRuangan = "Gedung Pasca Sarjana" },
            new Backend.Models.Ruangan { NamaRuangan = "Gedung SAW" }
        );
        context.SaveChanges();
        Console.WriteLine("Data Ruangan berhasil ditambahkan!");
    }

    // 2. SEED DATA PEMINJAMAN (Contoh Transaksi)
    if (!context.Peminjamans.Any())
    {
        context.Peminjamans.AddRange(
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Refa Brillian", 
                NamaRuangan = "Gedung D4", 
                TanggalMulai = DateTime.Now, 
                TanggalSelesai = DateTime.Now.AddHours(2), 
                Status = "Disetujui",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Andi Saputra", 
                NamaRuangan = "Gedung D3", 
                TanggalMulai = DateTime.Now.AddDays(1), 
                TanggalSelesai = DateTime.Now.AddDays(1).AddHours(3), 
                Status = "Menunggu",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Siti Aminah", 
                NamaRuangan = "Gedung Pasca Sarjana", 
                TanggalMulai = DateTime.Now.AddDays(2), 
                TanggalSelesai = DateTime.Now.AddDays(2).AddHours(1), 
                Status = "Menunggu",
                IsDeleted = false 
            },
            new Backend.Models.Peminjaman 
            { 
                NamaPeminjam = "Budi Doremi", 
                NamaRuangan = "Gedung SAW", 
                TanggalMulai = DateTime.Now.AddDays(3), 
                TanggalSelesai = DateTime.Now.AddDays(3).AddHours(4), 
                Status = "Ditolak",
                IsDeleted = false 
            }
        );
        context.SaveChanges();
        Console.WriteLine("Data Peminjaman berhasil ditambahkan!");
    }
}
    


app.Run();
