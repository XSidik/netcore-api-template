using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class JwtUserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly List<string> _allowedPaths = new()
    {
        "/api/auth/login",
        "/swagger",
        "/swagger/index.html",
        "/swagger/v1/swagger.json"
    };

    public JwtUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Skip authentication check for allowed paths
        if (_allowedPaths.Contains(context.Request.Path.Value!))
        {
            await _next(context);
            return;
        }

        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token validation failed");
            return;
        }

        await _next(context);
    }
}
