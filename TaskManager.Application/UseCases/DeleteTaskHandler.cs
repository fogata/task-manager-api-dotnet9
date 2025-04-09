using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class DeleteTaskHandler(ITaskRepository _taskRepo, IUnitOfWork _unitOfWork)
{

    public async Task<bool> HandleAsync(DeleteTaskCommand command)
    {
        var task = await _taskRepo.GetByIdAsync(command.TaskId);
        if (task is null) return false;

        await _taskRepo.RemoveAsync(task);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
