using FluentValidation;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(x => x.Username).NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(255).WithMessage("{PropertyName} must be {MaxLength} or less");

        RuleFor(x => x)
            .MustAsync(IsUsernameUnique).WithMessage("{PropertyName} already exists");
    }

    private Task<bool> IsUsernameUnique(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        return _userRepository.IsUsernameUnique(command.Username, command.Id);
    }
}