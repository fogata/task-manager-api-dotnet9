using Microsoft.EntityFrameworkCore;
using TaskManager.Api.Endpoints;
using TaskManager.Api.Middleware;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }

    public static IEndpointRouteBuilder MapAppEndpoints(this WebApplication app)
    {
        app.MapUserEndPoints();
        app.MapProjectEndpoints();
        app.MapTaskEndpoints();
        app.MapReportEndpoints();
        return app;
    }

    public static async Task<IApplicationBuilder> MigrateDatabaseIfNeeded(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        if (env.IsProduction())
        {
            await db.Database.MigrateAsync();
        }

        return app;
    }
}
