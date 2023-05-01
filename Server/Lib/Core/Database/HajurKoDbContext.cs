using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Core.Database;

public class HajurKoDbContext : IdentityDbContext<User>
{
    public HajurKoDbContext(DbContextOptions<HajurKoDbContext> options) : base(options)
    {
    }

    public DbSet<Car> Cars { get; set; }
    public DbSet<Damage> Damages { get; set; }
    public DbSet<Offer> Offers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    
    public DbSet<RentalRequest> RentalRequests { get; set; }
    public DbSet<User> Users { get; set; }
}