using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Core.Database;
using HajurKoCarRental.Shared.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Server.Lib.Features.Payments.Views;

class PaymentsView : BaseView
{
    public List<Payment> GetAllPayments(HajurKoDbContext context, int page = 0)
    {
        const int pageSize = Config.PageSize;
        return context.Payments.Skip(page * pageSize).Take(pageSize).ToList();
    }

    public Payment GetPayment(int id, HajurKoDbContext context)
    {
        var payment = context.Payments.FirstOrDefault(payment => payment.Id == id);
        if (payment is null) throw new KeyNotFoundException($"Payment with ID {id} not found");
        return payment;
    }

    public async Task CreatePaymentAsync(Payment payment, HajurKoDbContext context)
    {
        await context.Payments.AddAsync(payment);
        await context.SaveChangesAsync();
    }

    public async Task UpdatePaymentAsync(Payment payment, HajurKoDbContext context)
    {
        context.Entry(payment).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeletePaymentAsync(int id, HajurKoDbContext context)
    {
        var payment = await context.Payments.FirstOrDefaultAsync(payment => payment.Id == id);
        if (payment == null) throw new KeyNotFoundException($"Payment with ID {id} not found");

        context.Payments.Remove(payment);
        await context.SaveChangesAsync();
    }
}