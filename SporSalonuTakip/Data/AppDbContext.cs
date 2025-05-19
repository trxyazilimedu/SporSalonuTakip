using Microsoft.EntityFrameworkCore;
using SporSalonuTakip.Models;

namespace SporSalonuTakip.Data;

internal class AppDbContext : DbContext
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Membership> Memberships => Set<Membership>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var conn = "server=localhost;database=spor_salon;user=root;password=;";
        options.UseMySql(conn, ServerVersion.AutoDetect(conn));
    }
}
