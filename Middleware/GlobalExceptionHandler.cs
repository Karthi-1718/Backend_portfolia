using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace PortfolioApi.Middleware;

// Catches any error that was not handled elsewhere and returns a tidy JSON answer
// instead of a stack trace. Details go to the log, not to the visitor.
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Unhandled exception while processing {Path}", httpContext.Request.Path);

        var (status, title) = exception is SqlException
            ? (StatusCodes.Status503ServiceUnavailable, "The database is not available right now.")
            : (StatusCodes.Status500InternalServerError, "Something went wrong on the server.");

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = "Please try again in a moment."
        }, cancellationToken);

        return true; // we handled it
    }
}
