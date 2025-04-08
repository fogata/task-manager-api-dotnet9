using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateTaskHandler
{
    private readonly ITaskRepository _taskRepo;
    private readonly IProjectRepository _projectRepo;

    public CreateTaskHandler(ITaskRepository taskRepo, IProjectRepository projectRepo)
    {
        _taskRepo = taskRepo;
        _projectRepo = projectRepo;
    }

    public async Task<TaskItem> HandleAsync(CreateTaskCommand command)
    {
        var project = await _projectRepo.GetByIdAsync(command.ProjectId);
        if (project is null)
            throw new Exception("Projeto não encontrado");

        if (project.Tasks.Count >= 20)
            throw new Exception("Limite de 20 tarefas por projeto atingido");

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            ProjectId = command.ProjectId,
            Title = command.Title,
            Description = command.Description,
            DueDate = command.DueDate,
            Priority = command.Priority,
            Status = Core.Enums.TaskStatus.Pending
        };

        await _taskRepo.AddAsync(task);
        return task;
    }
}
