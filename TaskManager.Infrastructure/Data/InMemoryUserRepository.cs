using System.Diagnostics.CodeAnalysis;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Data;

[ExcludeFromCodeCoverage(Justification = "This is an in-memory repository for testing purposes.")]
public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public Task AddAsync(User user)
    {
        user.Id = Guid.NewGuid();
        _users.Add(user);
        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_users.FirstOrDefault(u => u.Id == id));
    }

    public Task<IEnumerable<User>> GetAllAsync() => Task.FromResult(_users.AsEnumerable());
}
