public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
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

            // VERY IMPORTANT
            if (context.Response.HasStarted)
            {
                _logger.LogWarning("Response already started, cannot handle error");
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // 🔹 AJAX / API calls → JSON
            if (IsAjaxRequest(context))
            {
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    System.Text.Json.JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = "Something went wrong. Please try again."
                    })
                );

                return; // ⛔ STOP PIPELINE
            }

            // 🔹 Normal MVC page → Redirect
            context.Response.Redirect("/Home/Error");
            return; // ⛔ STOP PIPELINE
        }
    }

    private bool IsAjaxRequest(HttpContext context)
    {
        return context.Request.Headers["X-Requested-With"] == "XMLHttpRequest"
            || context.Request.Headers["Accept"].ToString().Contains("application/json");
    }
}
