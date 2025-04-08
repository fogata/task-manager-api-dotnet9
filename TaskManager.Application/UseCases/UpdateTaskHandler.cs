using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class UpdateTaskHandler
{
    private readonly ITaskRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTaskHandler(ITaskRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TaskItem?> HandleAsync(UpdateTaskCommand command)
    {
        var task = await _repository.GetByIdAsync(command.TaskId);
        if (task is null) return null;

        void AddHistory(string field, string oldVal, string newVal)
        {
            if (oldVal != newVal)
            {
                task.UpdateHistory.Add(new TaskUpdateHistory
                {
                    Id = Guid.NewGuid(),
                    TaskItemId = task.Id,
                    Field = field,
                    OldValue = oldVal,
                    NewValue = newVal,
                    UpdatedAt = DateTime.UtcNow,
                    UserId = command.UserId
                });
            }
        }

        AddHistory("Title", task.Title, command.Title);
        AddHistory("Description", task.Description, command.Description);
        AddHistory("DueDate", task.DueDate.ToString("o"), command.DueDate.ToString("o"));
        AddHistory("Status", task.Status.ToString(), command.Status);

        task.Title = command.Title;
        task.Description = command.Description;
        task.DueDate = command.DueDate;

        if (Enum.TryParse<TaskStatus>(command.Status, out var newStatus))
            task.Status = (Core.Enums.TaskStatus)newStatus;

        await _unitOfWork.SaveChangesAsync();
        return task;
    }
}
