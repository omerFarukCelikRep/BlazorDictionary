﻿using BlazorDictionary.Common.Infrastracture.Exceptions;
using BlazorDictionary.Common.Infrastracture.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BlazorDictionary.Api.WebApi.Infrastructure.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
                                                                 bool includeExceptionDetails = false,
                                                                 bool useDefaultHandlingResponse = true,
                                                                 Func<HttpContext, Exception, Task>? handleException = null)
    {
        app.Run(context =>
        {
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

            if (!useDefaultHandlingResponse && handleException is null)
            {
                throw new ArgumentNullException(nameof(handleException), $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");
            }

            if (!useDefaultHandlingResponse && handleException is not null)
            {
                return handleException(context, exceptionObject.Error);
            }

            return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);
        });

        return app;
    }

    private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Internal server error occured!";

        if (exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized;
        }

        if (exception is DatabaseValidationException)
        {
            var validationResponse = new ValidationResponseModel(exception.Message);

            await WriteResponse(context, statusCode, validationResponse);
        }

        var response = new
        {
            HttpStatusCode = (int)statusCode,
            Detail = includeExceptionDetails ? exception.ToString() : message
        };

        await WriteResponse(context, statusCode, response);
    }

    private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObject)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(responseObject);
    }
}
