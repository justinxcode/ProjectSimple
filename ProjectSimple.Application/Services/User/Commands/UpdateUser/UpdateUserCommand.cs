using MediatR;

namespace ProjectSimple.Application.Services.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Unit>
{
    public long Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
