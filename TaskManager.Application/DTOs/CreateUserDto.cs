using TaskManager.Core.Entities;

namespace TaskManager.Application.DTOs;

public record CreateUserDto(string Username)
{
    public static explicit operator User(CreateUserDto dto)
        => new() { Username = dto.Username };
}
