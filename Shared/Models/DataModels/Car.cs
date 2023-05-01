using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;

namespace HajurKoCarRental.Shared.Models.DataModels;

public enum FuelType
{
    Petrol,
    Diesel,
    Electric,
    Hybrid
}

public enum TransmissionType
{
    Manual,
    Automatic,
    SemiAutomatic
}

public record Car
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Car name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Car brand is required")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "Car model is required")]
    public string Model { get; set; }

    [Required(ErrorMessage = "Car year is required")]
    [Range(1900, int.MaxValue, ErrorMessage = "Year must be after 1900")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Car color is required")]
    public string Color { get; set; }

    [Required(ErrorMessage = "Car mileage is required")]
    public int Mileage { get; set; }

    [Required(ErrorMessage = "Car fuel type is required")]
    public FuelType FuelType { get; set; }

    [Required(ErrorMessage = "Car transmission type is required")]
    public TransmissionType TransmissionType { get; set; } = TransmissionType.Manual;

    [Required(ErrorMessage = "Car rental rate is required")]
    public decimal RentalRate { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? PhotoUrl { get; set; }

    // [NotMapped]
    // [BindProperty(Name = "photo", SupportsGet = false)]
    // public IFormFile? Photo { get; set; }
}