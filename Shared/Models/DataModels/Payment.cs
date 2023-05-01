using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HajurKoCarRental.Shared.Models.DataModels;

public enum PaymentMethod
{
    Cash,
    Khalti,
    BankTransfer,
}

public record Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; init; }

    [Required(ErrorMessage = "Payment method is required")]
    public PaymentMethod Method { get; init; }

    [Required(ErrorMessage = "Payment amount is required")]
    public decimal Amount { get; init; }

    [Required(ErrorMessage = "Payment date is required")]
    public DateTime Date { get; init; }

    [Required(ErrorMessage = "Rental ID is required")]
    [ForeignKey("Rental")]
    public int RentalId { get; init; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Rental Rental { get; init; }
}