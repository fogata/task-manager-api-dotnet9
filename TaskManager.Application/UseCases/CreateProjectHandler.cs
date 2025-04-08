using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateProjectHandler
{
    private readonly IProjectRepository _repository;

    public CreateProjectHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<Project> HandleAsync(CreateProjectCommand command)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId
        };

        await _repository.AddAsync(project);
        return project;
    }
}
