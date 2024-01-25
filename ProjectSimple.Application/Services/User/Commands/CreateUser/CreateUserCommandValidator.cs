using FluentValidation;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {

        _userRepository = userRepository;

        RuleFor(x => x.Username).NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .MaximumLength(255).WithMessage("{PropertyName} must be {MaxLength} or less")
            .MustAsync(IsUsernameUnique).WithMessage("{PropertyName} already exists");
    }
    private async Task<bool> IsUsernameUnique(string username, CancellationToken cancellationToken)
    {
        return await _userRepository.IsUsernameUnique(username);
    }
}