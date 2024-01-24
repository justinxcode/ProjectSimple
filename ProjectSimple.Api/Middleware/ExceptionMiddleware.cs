using ProjectSimple.Api.Models;
using ProjectSimple.Application.Exceptions;
using System.Net;

namespace ProjectSimple.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ExceptionMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Status = (int)statusCode,
                    Type = nameof(BadRequestException),
                    Title = badRequestException.Message,
                    Detail = badRequestException.InnerException?.Message,
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Title = notFoundException.Message,
                    Detail = notFoundException.InnerException?.Message 
                };
                break;
            default:
                problem = new CustomProblemDetails
                {
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Title = ex.Message,
                    Detail = ex.StackTrace
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}