using FluentValidation.Results;
using ProjectSimple.Application.Validations;

namespace ProjectSimple.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, CustomValidationResult customValidationResult) : base(message)
    {
        ValidationErrorsList = customValidationResult.ErrorsList;
    }

    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.ToDictionary();
    }

    public List<string> ValidationErrorsList { get; set; }

    public IDictionary<string, string[]> ValidationErrors { get; set; }

}