using SporSalonuTakip.Models;
using SporSalonuTakip.Data;
using Microsoft.EntityFrameworkCore;

namespace SporSalonuTakip.Services;

internal static class MembershipService
{
    public static void AddMembership()
    {
        using var context = new AppDbContext();

        Console.Write("TC Kimlik No: ");
        var nationalId = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nationalId))
        {
            Console.WriteLine("❌ TC Kimlik No boş olamaz.");
            return;
        }

        var member = context.Members
            .Include(m => m.Memberships)
            .FirstOrDefault(m => m.NationalId == nationalId);

        if (member == null)
        {
            Console.WriteLine("❌ Bu TC Kimlik Numarasına sahip bir üye bulunamadı.");
            return;
        }

        Console.Write("Kaç günlük üyelik tanımlansın? ");
        if (!int.TryParse(Console.ReadLine(), out int days) || days <= 0)
        {
            Console.WriteLine("❌ Geçersiz gün sayısı.");
            return;
        }

        var today = DateTime.Now;
        var lastMembership = member.Memberships
            .OrderByDescending(m => m.EndDate)
            .FirstOrDefault();

        var startDate = today;
        if (lastMembership != null && lastMembership.EndDate >= today)
        {
            startDate = lastMembership.EndDate.AddDays(1);
        }

        var newMembership = new Membership
        {
            Type = $"Günlük ({days})",
            StartDate = startDate,
            EndDate = startDate.AddDays(days - 1),
            MemberId = member.Id
        };

        context.Memberships.Add(newMembership);
        context.SaveChanges();

        Console.WriteLine($"✅ {member.FullName} için üyelik tanımlandı. Bitiş: {newMembership.EndDate:dd.MM.yyyy}");
    }
    public static void CancelMembership()
    {
        using var context = new AppDbContext();

        Console.Write("TC Kimlik No: ");
        var nationalId = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nationalId))
        {
            Console.WriteLine("❌ TC Kimlik No boş olamaz.");
            return;
        }

        var member = context.Members
            .Include(m => m.Memberships)
            .FirstOrDefault(m => m.NationalId == nationalId);

        if (member == null)
        {
            Console.WriteLine("❌ Üye bulunamadı.");
            return;
        }

        if (member.Memberships.Count == 0)
        {
            Console.WriteLine("ℹ️ Bu üyenin kayıtlı üyeliği bulunmuyor.");
            return;
        }

        Console.WriteLine($"\n📅 {member.FullName} için Üyelikler:");
        foreach (var m in member.Memberships.OrderByDescending(m => m.StartDate))
        {
            Console.WriteLine($"ID: {m.Id} | Başlangıç: {m.StartDate:dd.MM.yyyy} | Bitiş: {m.EndDate:dd.MM.yyyy} | Tür: {m.Type}");
        }

        Console.Write("\nSilmek istediğiniz üyelik ID'sini girin: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("❌ Geçersiz ID.");
            return;
        }

        var membership = member.Memberships.FirstOrDefault(m => m.Id == id);
        if (membership == null)
        {
            Console.WriteLine("❌ Bu ID'ye ait üyelik bulunamadı.");
            return;
        }

        context.Memberships.Remove(membership);
        context.SaveChanges();

        Console.WriteLine($"✅ {member.FullName} için ID {id} olan üyelik başarıyla iptal edildi.");
    }

    public static void ListMemberships()
    {
        using var context = new AppDbContext();

        Console.Write("TC Kimlik No (Hepsi için boş bırak): ");
        var nationalId = Console.ReadLine()?.Trim();

        List<Membership> memberships;

        if (!string.IsNullOrWhiteSpace(nationalId))
        {
            memberships = context.Memberships
                .Include(x => x.Member)
                .Where(m => m.Member!.NationalId == nationalId)
                .ToList();
        }
        else
        {
            memberships = context.Memberships
                .Include(x => x.Member)
                .ToList();
        }

        if (memberships.Count == 0)
        {
            Console.WriteLine("📭 Üyelik bulunamadı.");
            return;
        }

        Console.WriteLine("\n📋 Üyelik Listesi:");
        foreach (var ms in memberships)
        {
            Console.WriteLine($"ID: {ms.Id} | Üye: {ms.Member?.FullName} | Tür: {ms.Type} | Başlangıç: {ms.StartDate:dd.MM.yyyy} | Bitiş: {ms.EndDate:dd.MM.yyyy}");
        }

    }
    public static void ViewMemberDetails()
    {
        using var context = new AppDbContext();

        Console.Write("TC Kimlik No: ");
        var nationalId = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nationalId))
        {
            Console.WriteLine("❌ TC Kimlik No boş olamaz.");
            return;
        }

        var member = context.Members
            .Include(m => m.Memberships)
            .FirstOrDefault(m => m.NationalId == nationalId);

        if (member == null)
        {
            Console.WriteLine("❌ Üye bulunamadı.");
            return;
        }

        Console.WriteLine($"\n👤 Üye Bilgisi");
        Console.WriteLine($"Ad Soyad: {member.FullName}");
        Console.WriteLine($"Telefon: {member.PhoneNumber}");
        Console.WriteLine($"Katılım Tarihi: {member.JoinDate:dd.MM.yyyy}");

        if (member.Memberships.Count == 0)
        {
            Console.WriteLine("ℹ️ Bu üyenin kayıtlı üyeliği yok.");
            return;
        }

        Console.WriteLine("\n📅 Üyelik Bilgileri:");
        var today = DateTime.Now.Date;

        foreach (var m in member.Memberships.OrderByDescending(m => m.StartDate))
        {
            string durum;

            if (today < m.StartDate.Date)
            {
                var gun = (m.StartDate.Date - today).Days;
                durum = $"⏳ {gun} gün sonra başlayacak";
            }
            else if (today >= m.StartDate.Date && today <= m.EndDate.Date)
            {
                var kalan = (m.EndDate.Date - today).Days + 1;
                durum = $"✅ Kalan: {kalan} gün";
            }
            else
            {
                durum = "❌ Süresi dolmuş";
            }

            Console.WriteLine($"- Başlangıç: {m.StartDate:dd.MM.yyyy} | Bitiş: {m.EndDate:dd.MM.yyyy} | Tür: {m.Type} | {durum}");
        }
    }

}
