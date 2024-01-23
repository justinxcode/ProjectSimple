using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Services.User.Commands.CreateUser;
using ProjectSimple.Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSimple.Application.Services.User.Commands.UpdateUser;

public class UpdateUserCommandValidator
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ValidationResult> ValidateAsync(UpdateUserCommand command)
    {
        // Validation
        var result = new ValidationResult();

        if (!await IsUsernameUnique(command))
        {
            result.Errors.Add("Username already exists");
        }

        return result;
    }

    private Task<bool> IsUsernameUnique(UpdateUserCommand command)
    {
        return _userRepository.IsUsernameUnique(command.Username, command.Id);
    }
}