using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class GetPerformanceReportHandler
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;

    public GetPerformanceReportHandler(ITaskRepository taskRepository, IUserRepository userRepository)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<object>> HandleAsync()
    {
        var allUsers = await _userRepository.GetAllAsync();
        var report = new List<object>();

        foreach (var user in allUsers)
        {
            var completedCount = await _taskRepository.CountCompletedByUserLast30Days(user.Id);
            report.Add(new
            {
                user.Id,
                user.Username,
                CompletedTasksLast30Days = completedCount
            });
        }

        return report;
    }
}
