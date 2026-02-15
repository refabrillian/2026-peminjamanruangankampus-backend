# 2026-peminjamanruangankampus-backend

## Description
Sistem informasi berbasis web yang berfungsi untuk mengelola reservasi dan peminjaman ruangan di lingkungan kampus guna menghindari jadwal bentrok dan mendokumentasikan penggunaan ruangan secara digital.

## Features
- **Manajemen Reservasi (CRUD):** Tambah, lihat, dan hapus data peminjaman ruangan.
- **Soft Delete:** Menghapus data secara logis tanpa menghilangkan record dari database.
- **Database Seeding:** Menyediakan data awal untuk keperluan testing dan pengembangan.
- **API Endpoints:** Menyediakan antarmuka komunikasi data bagi aplikasi Frontend.

## Tech Stack
- **Framework:** .NET 10.0 (ASP.NET Core)
- **Database:** SQLite
- **ORM:** Entity Framework Core

## Environment Variables
Aplikasi ini memerlukan pengaturan variabel lingkungan berikut di file `.env`:
- `DB_CONNECTION`: String koneksi database (Contoh: `Data Source=peminjaman.db`)
- `ASPNETCORE_URLS`: Port aplikasi berjalan (Contoh: `http://localhost:5009`)

## Installation
1. Clone repositori ini.
2. Salin file `.env.example` menjadi `.env`.
3. Jalankan perintah restore:
   ```bash
   dotnet restore

## Usage
Untuk menjalankan server backend:
      ```Bash
   dotnet run

## License
Distributed under the MIT License. 