using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetTaskHistoryHandler
{
    private readonly ITaskHistoryRepository _repository;

    public GetTaskHistoryHandler(ITaskHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskHistory>> HandleAsync(GetTaskHistoryQuery query)
    {
        var task = await _repository.GetByTaskIdAsync(query.TaskId);
        return task.OrderByDescending(h => h.UpdatedAt) ?? Enumerable.Empty<TaskHistory>();
    }
}
