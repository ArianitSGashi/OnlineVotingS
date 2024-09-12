using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OnlineVotingS.Domain.CostumExceptions;

namespace OnlineVotingS.API.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string message;
        var exceptionType = exception.GetType();

        switch (exception)
        {
            case DuplicateCandidateException:
                status = HttpStatusCode.Conflict;
                message = "Duplicate candidate";
                break;
            case UnauthorizedAccessException:
                status = HttpStatusCode.Unauthorized;
                message = "Unauthorized access";
                break;
            case KeyNotFoundException:
                status = HttpStatusCode.NotFound;
                message = "Resource not found";
                break;
            case ArgumentException or ArgumentNullException or ArgumentOutOfRangeException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
            case NotImplementedException:
                status = HttpStatusCode.NotImplemented;
                message = "A requested feature is not implemented";
                break;
            case TimeoutException:
                status = HttpStatusCode.RequestTimeout;
                message = "The request timed out";
                break;
            case InvalidOperationException:
                status = HttpStatusCode.Conflict;
                message = exception.Message;
                break;
            case BadHttpRequestException badRequestException:
                status = HttpStatusCode.BadRequest;
                message = badRequestException.Message;
                break;
            default:
                status = HttpStatusCode.InternalServerError;
                message = "An unexpected error occurred";
                break;
        }

        var problemDetails = new ProblemDetails
        {
            Status = (int)status,
            Title = message,
            Detail = exception.Message,
            Type = $"https://httpstatuses.com/{(int)status}"
        };

        var result = JsonSerializer.Serialize(problemDetails);
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)status;

        _logger.LogError(exception, "An error occurred: {Message}", message);

        return context.Response.WriteAsync(result);
    }
}