using HajurKoCarRental.Server.Lib.Core;
using HajurKoCarRental.Server.Lib.Features.Documents.Views;
using HajurKoCarRental.Shared.Routing;

namespace HajurKoCarRental.Server.Lib.Features.Documents.Mappings;

class DocumentMappings : BaseMappings<DocumentsView>
{
    public override void AddMappings(WebApplication app)
    {
        app.MapPost(ApiRoutes.DocumentsUploadRoute, () => "Hello World!");
        app.MapGet(ApiRoutes.DocumentsDownloadRoute, () => "Hello World!");
    }
}