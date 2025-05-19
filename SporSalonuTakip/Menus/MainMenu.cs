using SporSalonuTakip.Services;

namespace SporSalonuTakip.Menus;

internal static class MainMenu
{
    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("🏋️  SPOR SALONU TAKİP SİSTEMİ");
            Console.WriteLine("═════════════════════════════\n");
            Console.WriteLine("👥 1 - Müşteri Listele");
            Console.WriteLine("➕ 2 - Müşteri Ekle");
            Console.WriteLine("❌ 3 - Salon Üyeliği İptal Et");
            Console.WriteLine("📄 4 - Salon Üyeliklerini Görüntüle");
            Console.WriteLine("🆕 5 - Yeni Salon Üyeliği Ekle");
            Console.WriteLine("✏️ 6 - Üye Bilgisi Güncelle");
            Console.WriteLine("🔍 7 - TC ile Üye ve Üyelik Detayı");
            Console.WriteLine("🚪 0 - Çıkış");
            Console.Write("\n🔸 Seçiminiz: ");

            var secim = Console.ReadLine();

            switch (secim)
            {
                case "1": MemberService.ListMembers(); break;
                case "2": MemberService.AddMember(); break;
                case "3": MembershipService.CancelMembership(); break;
                case "4": MembershipService.ListMemberships(); break;
                case "5": MembershipService.AddMembership(); break;
                case "6": MemberService.UpdateMember(); break;
                case "7": MembershipService.ViewMemberDetails(); break;
                case "0": return;
                default: Console.WriteLine("❌ Geçersiz seçim!"); break;
            }

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
