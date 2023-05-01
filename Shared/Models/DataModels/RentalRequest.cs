using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace HajurKoCarRental.Shared.Models.DataModels;

public record RentalRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [ForeignKey("User")]
    public int CustomerId { get; init; }

    [ForeignKey("Car")]
    public int CarId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public bool IsApproved { get; init; }

    public virtual User? Customer { get; init; }
    
    public virtual Car Car { get; init; }
    
    public class RentalRequestValidator : AbstractValidator<RentalRequest>
    {
        public RentalRequestValidator()
        {
            RuleFor(r => r.CustomerId)
                .NotNull()
                .WithMessage("Customer is required");

            RuleFor(r => r.CarId)
                .NotNull()
                .WithMessage("Car is required");

            RuleFor(r => r.StartDate)
                .NotNull()
                .WithMessage("Start date is required");

            RuleFor(r => r.EndDate)
                .NotNull()
                .WithMessage("End date is required")
                .GreaterThanOrEqualTo(r => r.StartDate)
                .WithMessage("End date must be greater than or equal to start date");

            RuleFor(r => r.IsApproved)
                .NotNull()
                .WithMessage("Is approved flag is required");
        }
    }

}
