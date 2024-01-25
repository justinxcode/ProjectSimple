using MediatR;

namespace ProjectSimple.Application.Services.User.Commands.CreateUser;

public class CreateUserCommand : IRequest<long>
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}