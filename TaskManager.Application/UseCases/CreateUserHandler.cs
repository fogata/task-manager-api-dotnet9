using Microsoft.Extensions.Logging;
using TaskManager.Application.DTOs;
using TaskManager.Application.UseCases.Commands;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;

namespace TaskManager.Application.UseCases;

public class CreateUserHandler(IUserRepository _repository, ILogger<CreateUserHandler> _logger)
{
    public async Task<UserDto> HandleAsync(CreateUserCommand command)
    {
        var user = (User)command.Dto;
        await _repository.AddAsync(user);
        _logger.LogInformation("Usuário criado: {Username} (ID: {UserId})", user.Username, user.Id);
        return (UserDto)user;
    }
}
