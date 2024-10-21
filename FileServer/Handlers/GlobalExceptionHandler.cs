using System.Net;
using Microsoft.AspNetCore.Diagnostics;

namespace FileServer.Handlers;

public class GlobalExceptionHandler: IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken token)
    {
        context.Response.StatusCode = exception switch
        {
            //FileNotFoundException _ => StatusCodes.Status404NotFound,
            KeyNotFoundException _ => StatusCodes.Status404NotFound,
            UnauthorizedAccessException _ => StatusCodes.Status401Unauthorized,
            IOException _ => StatusCodes.Status500InternalServerError,
            FormatException _ => StatusCodes.Status400BadRequest,
        };

        var Response = new
        {
            StatusCode = context.Response.StatusCode,
            Details = exception.Message
        };
        
        await context.Response.WriteAsync(Response.ToString(), token);
        return true;
    }
}