using TaskManager.Core.Entities;

namespace TaskManager.Core.Interfaces;

public interface ITaskHistoryRepository
{
    Task<IEnumerable<TaskHistory>> GetByTaskIdAsync(Guid taskId);
    Task AddAsync(TaskHistory history);
}