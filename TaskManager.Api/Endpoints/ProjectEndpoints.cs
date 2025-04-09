using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Application.UseCases.Queries;

namespace TaskManager.Api.Endpoints;

public static class ProjectEndpoints
{
    public static void MapProjectEndpoints(this WebApplication app)
    {
        app.MapPost("/users/{userId}/projects", async (
            Guid userId,
            CreateProjectRequest request,
            CreateProjectHandler handler,
            IValidator<CreateProjectCommand> validator) =>
        {
            var command = new CreateProjectCommand(userId, request.name);
            var validation = await validator.ValidateAsync(command);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var result = await handler.HandleAsync(command);
            return Results.Created($"/projects/{result.Id}", result);
        })
        .WithName("CreateProject")
        .WithSummary("Cria um novo projeto para o usuário especificado");

        app.MapGet("/users/{userId}/projects", async (Guid userId, GetProjectsByUserHandler handler) =>
        {
            var query = new GetProjectsByUserQuery(userId);
            var result = await handler.HandleAsync(query);
            return Results.Ok(result);
        })
        .WithName("GetProjectsByUser")
        .WithSummary("Lista todos os projetos de um usuário");

        app.MapDelete("/projects/{projectId}", async (
            Guid projectId,
            DeleteProjectHandler handler) =>
                {
                    var (success, message) = await handler.HandleAsync(new DeleteProjectCommand(projectId));
                    return success ? Results.Ok(new { message }) : Results.BadRequest(new { message });
                })
            .WithName("DeleteProject")
            .WithSummary("Remove um projeto se não houver tarefas pendentes");
    }
}
