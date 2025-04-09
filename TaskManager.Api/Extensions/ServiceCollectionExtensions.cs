using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManager.Application.UseCases;
using TaskManager.Application.Validators;
using TaskManager.Core.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
        services.AddScoped<ITaskCommentRepository, TaskCommentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<CreateUserHandler>();
        services.AddScoped<CreateProjectHandler>();
        services.AddScoped<CreateTaskHandler>();
        services.AddScoped<GetProjectsByUserHandler>();
        services.AddScoped<GetTasksByProjectHandler>();
        services.AddScoped<UpdateTaskHandler>();
        services.AddScoped<DeleteTaskHandler>();
        services.AddScoped<DeleteProjectHandler>();
        services.AddScoped<AddTaskCommentHandler>();
        services.AddScoped<GetPerformanceReportHandler>();
        services.AddScoped<GetTaskHistoryHandler>();

        services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();

        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }
}
