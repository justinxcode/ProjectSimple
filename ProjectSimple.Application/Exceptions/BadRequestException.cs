using ProjectSimple.Application.Validations;

namespace ProjectSimple.Application.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = validationResult.Errors;
        //ValidationErrors = validationResult.ToDictionary();
    }

    public List<string> ValidationErrors { get; set; }

    //public IDictionary<string, string[]> ValidationErrors { get; set; }

}