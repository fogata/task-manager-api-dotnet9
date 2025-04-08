using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetTaskHistoryHandler
{
    private readonly ITaskRepository _repository;

    public GetTaskHistoryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskUpdateHistory>> HandleAsync(GetTaskHistoryQuery query)
    {
        var task = await _repository.GetByIdAsync(query.TaskId);
        return task?.UpdateHistory.OrderByDescending(h => h.UpdatedAt) ?? Enumerable.Empty<TaskUpdateHistory>();
    }
}
