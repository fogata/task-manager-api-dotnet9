using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Infrastructure.Data;

public class TaskHistoryRepository : ITaskHistoryRepository
{
    private readonly AppDbContext _context;

    public TaskHistoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskHistory>> GetByTaskIdAsync(Guid taskId)
    {
        return await _context.TaskHistories
            .Where(h => h.TaskItemId == taskId)
            .OrderByDescending(h => h.Id)
            .ToListAsync();
    }

    public async Task AddAsync(TaskHistory history)
    {
        await _context.TaskHistories.AddAsync(history);
    }
}

