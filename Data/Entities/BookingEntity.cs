using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class BookingEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Created {  get; set; } = DateTime.Now;

    public string EventName { get; set; } = null!;
    [Column(TypeName = "datetime2")]
    public DateTime EventDate {  get; set; }
    public string EventLocation { get; set; } = null!;

    public string PackageName { get; set; } = null!;
    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PackagePrice { get; set; }
    public string? Currency { get; set; }
    public int Amount { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; } = null!;
    public BookingUserEntity User { get; set; } = null!;

    [ForeignKey("Address")]
    public string AddressId { get; set; } = null!;
    public BookingAddressEntity Address { get; set;} = null!;
}
