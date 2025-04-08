using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetProjectsByUserHandler
{
    private readonly IProjectRepository _repository;

    public GetProjectsByUserHandler(IProjectRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Project>> HandleAsync(GetProjectsByUserQuery query)
    {
        return await _repository.GetByUserIdAsync(query.UserId);
    }
}
