using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace HajurKoCarRental.Shared.Models.DataModels;

public enum UserType
{
    Admin,
    Staff,
    Client,
    Anonymous,
}

// [Index(nameof(PhoneNumber), IsUnique = true)]
public class User : IdentityUser
{
    // [Required]
    // public string Address { get; init; }
    public UserType UserType { get; set; }
    
    public DateTime? LastOrderedAt { get; set; }
    public DateTime? LastSeenOn { get; set; }
    
    public byte[]? Profile { get; set; }
    
    //kyc
    public byte[]? Citizenship { get; set; }
    public byte[]? License { get; set; }
    
    public class Validator : AbstractValidator<User>
    {
        public Validator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username is required.");
            // RuleFor(u => u.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(u => u.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                .Matches("^[9][8,7,6]\\d{4}[\\s-]?\\d{4}$")
                .WithMessage("Phone number must be a valid Nepali phone number of 10 digits.");
            
            RuleFor(u => u.Profile)
                .Must(bytes => IsValidFileExtension(bytes, "png", "jpg"))
                .WithMessage("Invalid file format. Only png and jpg files are allowed.");
            
            RuleFor(u => u.Citizenship)
                .Must(bytes => IsValidFileExtension(bytes, "png", "pdf"))
                .WithMessage("Invalid file format. Only png and pdf files are allowed.");
            
            RuleFor(u => u.License)
                .Must(bytes => IsValidFileExtension(bytes, "png", "pdf"))
                .WithMessage("Invalid file format. Only png and pdf files are allowed.");
        }
        
        private bool IsValidFileExtension(byte[]? file, params string[] extensions)
        {
            if (file is null) return true;

            var extension = Path.GetExtension(GetBase64String(file));
            return extensions.Any(e => e.Equals(extension, StringComparison.OrdinalIgnoreCase));
        }
        
        private string GetBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}