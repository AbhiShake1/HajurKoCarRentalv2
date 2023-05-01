using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Payments.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Payments.Mappings;

class PaymentMappings : BaseMappings<PaymentsView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapPost(ApiRoutes.PaymentsRoute, View.CreatePaymentAsync);
        app.MapGet(ApiRoutes.PaymentsRoute, View.GetAllPayments);
        app.MapGet(ApiRoutes.PaymentRoute, View.GetPayment);
    }
}