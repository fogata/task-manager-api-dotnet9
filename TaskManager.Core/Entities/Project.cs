﻿using System.Text.Json.Serialization;

namespace TaskManager.Core.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
