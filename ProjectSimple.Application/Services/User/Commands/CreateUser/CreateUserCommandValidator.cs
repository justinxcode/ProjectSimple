using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.CreateUser;

public class CreateUserCommandValidator
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ValidationResult> ValidateAsync(CreateUserCommand command)
    {
        var result = new ValidationResult();

        // Validation
        if (!await IsUsernameUnique(command))
        {
            result.Errors.Add("Username already exists");
        }

        return result;
    }

    private Task<bool> IsUsernameUnique(CreateUserCommand command)
    {
        return _userRepository.IsUsernameUnique(command.Username);
    }
}