using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SporSalonuTakip.Models;

[Index(nameof(NationalId), IsUnique = true)] // TC Kimlik benzersiz olacak
internal class Member
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad zorunludur.")]
    [MaxLength(100)]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "Soyad zorunludur.")]
    [MaxLength(100)]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "TC Kimlik No zorunludur.")]
    [StringLength(11, MinimumLength = 11)]
    public string NationalId { get; set; } = "";

    [Required(ErrorMessage = "Telefon numarası zorunludur.")]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = "";

    public DateTime JoinDate { get; set; } = DateTime.Now;

    public List<Membership> Memberships { get; set; } = new();

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
}
