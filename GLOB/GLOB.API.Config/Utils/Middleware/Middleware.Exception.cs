namespace GLOB.API.Config.Middleware;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Localization;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;
    private readonly IStringLocalizer<GlobalExceptionMiddleware>? _localizer;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger,
        IHostEnvironment env,
        IStringLocalizer<GlobalExceptionMiddleware>? localizer = null)
    {
        _next = next;
        _logger = logger;
        _env = env;
        _localizer = localizer;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await HandleExceptionAsync(context, ex);
        }
    }

  private Task HandleExceptionAsync(HttpContext context, Exception ex)
{
    int statusCode = (int)HttpStatusCode.InternalServerError;
    string title;

    if (ex is Exception)
    {
        statusCode = 400;
        title = ex.Message;
    }
    else
    {
        title = _localizer?["An unexpected error occurred."] ?? "An unexpected error occurred.";
    }

    var detail = _env.IsDevelopment()
        ? $"Global Exception Middleware \n {ex.Message} \n {ex.StackTrace}"
        : $"Global Exception Middleware \n {ex.Message}";

    var problemDetails = new ProblemDetails
    {
        Title = title,
        Status = statusCode,
        Detail = detail,
        Instance = context.Request.Path
    };

    context.Response.ContentType = "application/problem+json";
    context.Response.StatusCode = statusCode;

    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    var json = JsonSerializer.Serialize(problemDetails, options);

    return context.Response.WriteAsync(json);
}

}
