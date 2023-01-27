using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Text.Json;

namespace ECommerceAPI.API.Extensions;

public static class ConfigureExceptionHandlerExtension {
    public static void ConfigureExceptionHandler<T>(this WebApplication webApplication, ILogger<T> logger) {
        webApplication.UseExceptionHandler(builder => {

            builder.Run(async context => {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if(contextFeature is not null) {
                    logger.LogError(contextFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message,
                        Title = "Hata alındı!"
                    }));
                }
            });
        });
    }
}