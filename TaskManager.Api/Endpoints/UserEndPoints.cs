using FluentValidation;
using TaskManager.Application.DTOs;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;

namespace TaskManager.Api.Endpoints;

public static class UserEndPoints
{
    public static void MapUserEndPoints(this WebApplication app)
    {
        app.MapPost("/users", async (
        CreateUserDto dto,
        CreateUserHandler handler,
        IValidator<CreateUserCommand> validator) =>
            {
                var command = new CreateUserCommand(dto);
                var validation = await validator.ValidateAsync(command);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                var result = await handler.HandleAsync(command);
                return Results.Created($"/users/{result.Id}", result);
            })
            .WithName("CreateUser")
            .WithSummary("Adiciona um usuário.");
    }
}
