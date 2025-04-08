
using TaskManager.Core.Entities;

namespace TaskManager.Application.DTOs;

public record UserDto(Guid Id, string Username)
{
    public static explicit operator UserDto(User user)
        => new(user.Id, user.Username);
}
