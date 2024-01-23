using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Validations;

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
        // Validation
        var result = new ValidationResult();

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