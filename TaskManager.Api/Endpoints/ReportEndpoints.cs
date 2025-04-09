using TaskManager.Application.UseCases;

namespace TaskManager.Api.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        app.MapGet("/reports/performance", async (
            HttpContext context,
            GetPerformanceReportHandler handler) =>
        {
            var role = context.Request.Headers["x-user-role"].ToString();
            if (!string.Equals(role, "Gerente", StringComparison.OrdinalIgnoreCase))
                return Results.Forbid();

            var result = await handler.HandleAsync();
            return Results.Ok(result);
        })
        .WithName("GetPerformanceReport")
        .WithSummary("Relatório de desempenho (tarefas concluídas por usuário nos últimos 30 dias)")
        .Produces<IEnumerable<object>>(StatusCodes.Status200OK);

    }
}
