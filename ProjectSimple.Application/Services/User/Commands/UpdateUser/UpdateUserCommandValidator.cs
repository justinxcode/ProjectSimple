using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Validations;

namespace ProjectSimple.Application.Services.User.Commands.UpdateUser;

public class UpdateUserCommandValidator
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<CustomValidationResult> ValidateAsync(UpdateUserCommand command)
    {
        // Validation
        var result = new CustomValidationResult();

        if (!await IsUsernameUnique(command))
        {
            result.ErrorsList.Add("Username already exists");
        }

        return result;
    }

    private Task<bool> IsUsernameUnique(UpdateUserCommand command)
    {
        return _userRepository.IsUsernameUnique(command.Username, command.Id);
    }
}