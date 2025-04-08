using TaskManager.Application.DTOs;
using TaskManager.Application.UseCases;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddScoped<CreateUserHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/users", async (CreateUserDto dto, CreateUserHandler handler) =>
{
    var command = new CreateUserCommand(dto);
    var result = await handler.HandleAsync(command);
    return Results.Created($"/users/{result.Id}", result);
});

app.Run();
