namespace TaskManager.Core.Entities;

public class TaskUpdateHistory
{
    public Guid Id { get; set; }
    public Guid TaskItemId { get; set; }
    public string Field { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }
    public Guid UserId { get; set; }
}
