﻿using System.Net;
using System.Runtime.CompilerServices;
using Contracts;
using Microsoft.AspNetCore.Diagnostics;
using Entities.ErrrorModel;
using Entities.Exceptions;

namespace CompanyEmployee.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionsHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature=context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null) 
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                        }.ToString());
                    }
                });
            });
        }
    }
}
