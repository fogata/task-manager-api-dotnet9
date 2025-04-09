using TaskManager.Application.UseCases.Queries;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetProjectsByUserHandler(IProjectRepository _repository)
{

    public async Task<IEnumerable<Project>> HandleAsync(GetProjectsByUserQuery query)
    {
        return await _repository.GetByUserIdAsync(query.UserId);
    }
}
