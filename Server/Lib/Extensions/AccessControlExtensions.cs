namespace HajurKoCarRental.Server.Lib.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

public static class AccessControlExtensions
{
    public static TBuilder UseTimeBasedAccessControl<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        var startTime = new TimeSpan(9, 0, 0); // 9:00 AM
        var endTime = new TimeSpan(17, 0, 0); // 5:00 PM

        var middleware = new Func<RequestDelegate, RequestDelegate>(next => async context =>
        {
            var currentTime = DateTime.Now.TimeOfDay;
            if (currentTime >= startTime && currentTime <= endTime)
            {
                await next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"This endpoint is only available between {startTime} and {endTime}");
            }
        });

        builder.Add(endpointBuilder =>
        {
            endpointBuilder.Metadata.Add(middleware);
        });

        return builder;
    }
}
