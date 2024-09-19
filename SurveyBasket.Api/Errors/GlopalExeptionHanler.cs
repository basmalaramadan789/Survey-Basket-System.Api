﻿using Microsoft.AspNetCore.Diagnostics;

namespace SurveyBasket.Api.Errors
{
    public class GlopalExeptionHanler(ILogger<GlopalExeptionHanler> logger) : IExceptionHandler
    {
        private readonly ILogger<GlopalExeptionHanler> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception,"something went wrong : {Message}",exception.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",

            };
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
             
            return true;








        }
    }
}

