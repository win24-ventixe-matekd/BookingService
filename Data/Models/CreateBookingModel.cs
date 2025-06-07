using System.ComponentModel.DataAnnotations;

namespace Data.Models;

public class CreateBookingModel
{
    // Event
    [Required]
    public string EventName { get; set; } = null!;
    [Required]
    public DateTime EventDate { get; set; }
    [Required]
    public string EventLocation { get; set; } = null!;

    // Package
    [Required]
    public string PackageName { get; set; } = null!;
    public decimal? PackagePrice { get; set; }
    public string? Currency { get; set; }
    [Required]
    public int Amount { get; set; }

    // User
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    public string Email { get; set; } = null!;

    // Address
    [Required]
    public string StreetName { get; set; } = null!;
    [Required]
    public string PostalCode { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
}
