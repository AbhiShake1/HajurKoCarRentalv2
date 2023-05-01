using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HajurKoCarRental.Shared.Models.DataModels;

public record Damage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; init; }

    [Required(ErrorMessage = "Damage description is required")]
    public string Description { get; init; }

    [Required(ErrorMessage = "Damage cost is required")]
    public decimal Cost { get; init; }

    [Required(ErrorMessage = "Car ID is required")]
    [ForeignKey("Car")]
    public int CarId { get; init; }

    public virtual Car Car { get; init; }
}