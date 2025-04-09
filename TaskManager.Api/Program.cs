using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManager.Api.Endpoints;
using TaskManager.Api.Infrastructure.Startup;
using TaskManager.Application.UseCases;
using TaskManager.Application.Validators;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

//Logs
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// Configurações e serviços
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Handlers
builder.Services.AddScoped<CreateUserHandler>();
builder.Services.AddScoped<CreateProjectHandler>();
builder.Services.AddScoped<CreateTaskHandler>();
builder.Services.AddScoped<GetProjectsByUserHandler>();
builder.Services.AddScoped<GetTasksByProjectHandler>();
builder.Services.AddScoped<UpdateTaskHandler>();
builder.Services.AddScoped<DeleteTaskHandler>();
builder.Services.AddScoped<DeleteProjectHandler>();
builder.Services.AddScoped<AddTaskCommentHandler>();
builder.Services.AddScoped<GetPerformanceReportHandler>();
builder.Services.AddScoped<GetTaskHistoryHandler>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Mapeamento modular de endpoints
app.MapUserEndPoints();
app.MapProjectEndpoints();
app.MapTaskEndpoints();
app.MapReportEndpoints();

//Migration
await app.EnsureDatabaseMigratedAsync();

app.Run();

public partial class Program { } // ← usado nos testes de integração.
