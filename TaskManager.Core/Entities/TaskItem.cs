using TaskManager.Core.Enums;

namespace TaskManager.Core.Entities;

public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public Enums.TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    public ICollection<TaskUpdateHistory> UpdateHistory { get; set; } = new List<TaskUpdateHistory>();
}
