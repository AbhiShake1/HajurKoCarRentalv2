using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Damages.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Damages.Mappings;

class DamageMappings : BaseMappings<DamagesView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapPost(ApiRoutes.DamagesRoute, View.CreateDamageAsync);
        app.MapGet(ApiRoutes.DamagesRoute, View.GetAllDamages);
        app.MapGet(ApiRoutes.DamageRoute, View.GetDamage);
        app.MapPut(ApiRoutes.DamageRoute, View.UpdateDamageAsync);
    }
}