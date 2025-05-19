using Microsoft.EntityFrameworkCore;
using SporSalonuTakip.Data;
using SporSalonuTakip.Models;

namespace SporSalonuTakip.Services;

internal static class MemberService
{
    public static void ListMembers()
    {
        using var context = new AppDbContext();
        var members = context.Members.ToList();

        Console.WriteLine("\n📋 Müşteri Listesi:");
        foreach (var m in members)
        {
            Console.WriteLine($"ID: {m.Id} | Ad: {m.FullName} | Katılım Tarihi: {m.JoinDate:dd.MM.yyyy}");
        }
    }

    public static void AddMember()
    {
        using var context = new AppDbContext();

        Console.Write("\nAd: ");
        var firstName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(firstName))
        {
            Console.WriteLine("❌ Ad boş olamaz.");
            return;
        }

        Console.Write("Soyad: ");
        var lastName = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(lastName))
        {
            Console.WriteLine("❌ Soyad boş olamaz.");
            return;
        }

        Console.Write("TC Kimlik No (11 hane): ");
        var nationalId = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(nationalId) || nationalId.Length != 11 || !nationalId.All(char.IsDigit))
        {
            Console.WriteLine("❌ Geçersiz TC Kimlik Numarası. 11 haneli olmalı ve sadece rakamlardan oluşmalı.");
            return;
        }

        if (context.Members.Any(m => m.NationalId == nationalId))
        {
            Console.WriteLine("❌ Bu TC Kimlik Numarası zaten kayıtlı.");
            return;
        }

        Console.Write("Telefon No: ");
        var phone = Console.ReadLine()?.Trim();
        if (string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine("❌ Telefon numarası boş olamaz.");
            return;
        }

        var member = new Member
        {
            FirstName = firstName,
            LastName = lastName,
            NationalId = nationalId,
            PhoneNumber = phone
        };

        context.Members.Add(member);
        context.SaveChanges();

        Console.WriteLine("✅ Müşteri başarıyla eklendi.");
    }

    public static void UpdateMember()
    {
        using var context = new AppDbContext();

        Console.Write("Güncellenecek Üyenin TC Kimlik No: ");
        var nationalId = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nationalId))
        {
            Console.WriteLine("❌ TC Kimlik No boş olamaz.");
            return;
        }

        var member = context.Members.FirstOrDefault(m => m.NationalId == nationalId);

        if (member == null)
        {
            Console.WriteLine("❌ Üye bulunamadı.");
            return;
        }

        Console.WriteLine($"\nŞu anki Bilgiler:");
        Console.WriteLine($"Ad: {member.FirstName}");
        Console.WriteLine($"Soyad: {member.LastName}");
        Console.WriteLine($"Telefon: {member.PhoneNumber}");

        Console.WriteLine("\nGüncellemek istediğiniz alanları girin (boş bırakırsanız değişmez):");

        Console.Write("Yeni Ad: ");
        var firstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(firstName))
            member.FirstName = firstName;

        Console.Write("Yeni Soyad: ");
        var lastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(lastName))
            member.LastName = lastName;

        Console.Write("Yeni Telefon: ");
        var phone = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(phone))
            member.PhoneNumber = phone;

        context.SaveChanges();
        Console.WriteLine("✅ Üye bilgisi güncellendi.");
    }

}
