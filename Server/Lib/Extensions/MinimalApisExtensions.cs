namespace HajurKoCarRental.Server.Lib.Extensions;

static class MinimalApisExtensions
{
    private static readonly string[] PatchVerb = { "PATCH" };

    public static IEndpointConventionBuilder MapPatch(
        this WebApplication app,
        string pattern,
        RequestDelegate requestDelegate)
    {
        return app.MapMethods(pattern, PatchVerb, requestDelegate);
    }

    public static IEndpointConventionBuilder MapPatch(
        this WebApplication app,
        string pattern,
        Delegate requestDelegate)
    {
        return app.MapMethods(pattern, PatchVerb, requestDelegate);
    }
}