using MediatR;

namespace ProjectSimple.Application.Services.User.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Unit>
{
    public long Id { get; set; }
}
