using SporSalonuTakip.Data;
using SporSalonuTakip.Menus;

namespace SporSalonuTakip;

internal class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        using var context = new AppDbContext();
        context.Database.EnsureCreated(); // Veritabanı yoksa oluştur

        MainMenu.Show();
    }
}
