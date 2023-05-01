using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HajurKoCarRental.Shared.Models.DataModels;

public enum RentalStatus
{
    Reserved,
    CheckedOut,
    Returned,
    Late,
    Cancelled
}

public record Rental
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required(ErrorMessage = "Customer is required")]
    [ForeignKey("User")]
    public int CustomerId { get; init; }

    [Required(ErrorMessage = "Car is required")]
    [ForeignKey("Car")]
    public int CarId { get; init; }

    [Required(ErrorMessage = "Start date is required")]
    public DateTime StartDate { get; init; }

    [Required(ErrorMessage = "End date is required")]
    public DateTime EndDate { get; init; }

    [Required(ErrorMessage = "Rental status is required")]
    public RentalStatus Status { get; init; }

    [Required(ErrorMessage = "Total cost is required")]
    public decimal TotalCost { get; init; }

    public DateTime? ReturnedDate { get; init; }

    public virtual User? Customer { get; init; }

    // let Car be overridden in derived class. will help in custom implementation if needed
    public virtual Car Car { get; init; }
}