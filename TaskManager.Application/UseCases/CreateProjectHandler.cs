using Microsoft.Extensions.Logging;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateProjectHandler(IProjectRepository _repository, ILogger<CreateProjectHandler> _logger)
{
    public async Task<Project> HandleAsync(CreateProjectCommand command)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId
        };

        await _repository.AddAsync(project);
        _logger.LogInformation("Projeto criado: {Name} (ID: {UserId})", project.Name, project.UserId);
        return project;
    }
}
