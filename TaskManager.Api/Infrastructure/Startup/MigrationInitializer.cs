using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Api.Infrastructure.Startup;
public static class MigrationInitializer
{
    public static async Task EnsureDatabaseMigratedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
}

