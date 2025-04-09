namespace TaskManager.Application.DTOs;

/// <summary>Nome do projeto a ser criado</summary>
public record CreateProjectRequest(string name = default!);
