using TaskManager.Application.Exceptions;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class UpdateTaskHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskHistoryRepository _taskHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskHandler(ITaskRepository repository, ITaskHistoryRepository taskHistoryRepository, IUnitOfWork unitOfWork)
    {
        _taskRepository = repository;
        _taskHistoryRepository = taskHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskItem> HandleAsync(UpdateTaskCommand command)
    {
        var task = await _taskRepository.GetByIdAsync(command.TaskId);

        if (task is null)
        {
            throw new NotFoundException("Tarefa não encontrada.");
        }

        var original = new TaskItem
        {
            Title = task.Title,
            Description = task.Description,
            DueDate = task.DueDate,
            Status = task.Status,
            Priority = task.Priority
        };

        task.Title = command.Title;
        task.Description = command.Description;
        task.DueDate = command.DueDate;

        if (Enum.TryParse<Core.Enums.TaskStatus>(command.Status, out var parsedStatus))
        {
            task.Status = parsedStatus;
        }
        else
        {
            throw new ArgumentException($"Invalid status value: {command.Status}");
        }

        
        var histories = new List<TaskHistory>();

        if (task.Title != original.Title)
            histories.Add(new TaskHistory
            {
                Id = Guid.NewGuid(),
                TaskItemId = task.Id,
                Field = "Title",
                OldValue = original.Title,
                NewValue = task.Title,
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId 
            });

        if (task.Description != original.Description)
            histories.Add(new TaskHistory
            {
                Id = Guid.NewGuid(),
                TaskItemId = task.Id,
                Field = "Description",
                OldValue = original.Description,
                NewValue = task.Description,
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId
            });

        if (task.DueDate != original.DueDate)
            histories.Add(new TaskHistory
            {
                Id = Guid.NewGuid(),
                TaskItemId = task.Id,
                Field = "DueDate",
                OldValue = original.DueDate.ToString("s"),
                NewValue = task.DueDate.ToString("s"),
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId
            });

        if (task.Status != original.Status)
            histories.Add(new TaskHistory
            {
                Id = Guid.NewGuid(),
                TaskItemId = task.Id,
                Field = "Status",
                OldValue = original.Status.ToString(),
                NewValue = task.Status.ToString(),
                UpdatedAt = DateTime.UtcNow,
                UserId = command.UserId
            });

        foreach (var history in histories)
        {
            await _taskHistoryRepository.AddAsync(history);
        }

        await _unitOfWork.SaveChangesAsync();

        return task;
    }
}
