using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Shared.Models.DataModels;

[Index(nameof(Code), IsUnique = true)]
public record Offer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; init; }

    [Required(ErrorMessage = "Offer name is required")]
    public string Name { get; init; }

    [Required(ErrorMessage = "Offer code is required")]
    public string Code { get; init; }

    [Required(ErrorMessage = "Discount rate is required")]
    public decimal DiscountRate { get; init; }

    public DateTime? ExpirationDate { get; init; }

    public bool IsActive { get; init; }
}