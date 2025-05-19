namespace SporSalonuTakip.Models;

internal class Membership
{
    public int Id { get; set; }
    public string Type { get; set; } = "Aylık";
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }

    public int MemberId { get; set; }
    public Member? Member { get; set; }
}
