using FluentValidation;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.UseCases.Queries;

namespace TaskManager.Api.Endpoints;

public static class TaskEndpoints
{
    public static void MapTaskEndpoints(this WebApplication app)
    {
        app.MapPost("/projects/{projectId}/tasks", async (
            Guid projectId,
            CreateTaskCommand body,
            CreateTaskHandler handler,
            IValidator<CreateTaskCommand> validator) =>
        {
            var command = body with { ProjectId = projectId };
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var result = await handler.HandleAsync(command);
            return Results.Created($"/tasks/{result.Id}", result);
        })
        .WithName("CreateTask")
        .WithSummary("Cria uma nova tarefa em um projeto específico");

        app.MapGet("/projects/{projectId}/tasks", async (Guid projectId, GetTasksByProjectHandler handler) =>
        {
            var query = new GetTasksByProjectQuery(projectId);
            var result = await handler.HandleAsync(query);
            return Results.Ok(result);
        })
        .WithName("GetTasksByProject")
        .WithSummary("Lista todas as tarefas de um projeto");

        app.MapPut("/tasks/{taskId}", async (
            Guid taskId,
            UpdateTaskCommand body,
            UpdateTaskHandler handler,
            IValidator<UpdateTaskCommand> validator) =>
                {
                    var command = body with { TaskId = taskId };
                    var validation = await validator.ValidateAsync(command);
                    if (!validation.IsValid)
                        return Results.BadRequest(validation.Errors);

                    var result = await handler.HandleAsync(command);
                    return result is null ? Results.NotFound() : Results.Ok(result);
                })
                    .WithName("UpdateTask")
                    .WithSummary("Atualiza os dados de uma tarefa e registra histórico");

        app.MapDelete("/tasks/{taskId}", async (
            Guid taskId,
            DeleteTaskHandler handler) =>
                {
                    var success = await handler.HandleAsync(new DeleteTaskCommand(taskId));
                    return success ? Results.NoContent() : Results.NotFound();
                })
                    .WithName("DeleteTask")
                    .WithSummary("Remove uma tarefa existente");

        app.MapGet("/tasks/{taskId}/history", async (
             Guid taskId,
             GetTaskHistoryHandler handler,
             IValidator<GetTaskHistoryQuery> validator) =>
                {
                    var query = new GetTaskHistoryQuery(taskId);
                    var validation = await validator.ValidateAsync(query);
                    if (!validation.IsValid)
                        return Results.BadRequest(validation.Errors);

                    var result = await handler.HandleAsync(query);
                    return Results.Ok(result);
                })
             .WithName("GetTaskHistory")
             .WithSummary("Retorna o histórico de alterações de uma tarefa");

        app.MapPost("/tasks/{taskId}/comments", async (
            Guid taskId,
            AddTaskCommentCommand body,
            AddTaskCommentHandler handler,
            IValidator<AddTaskCommentCommand> validator) =>
                {
                    var command = body with { TaskId = taskId };
                    var validation = await validator.ValidateAsync(command);
                    if (!validation.IsValid)
                        return Results.BadRequest(validation.Errors);

                    var success = await handler.HandleAsync(command);
                    return success ? Results.Ok() : Results.NotFound();
                })
                    .WithName("AddTaskComment")
                    .WithSummary("Adiciona um comentário a uma tarefa e registra no histórico");
    }
}
