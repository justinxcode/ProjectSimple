using FluentValidation;
using ProjectSimple.Application.Models;
using System.Reflection;

namespace ProjectSimple.Application.Validations;

public class SortConditionValidator : AbstractValidator<SortCondition>
{
    private readonly Type _modelType;

    public SortConditionValidator(Type modelType)
    {
        _modelType = modelType;

        RuleFor(x => x.Property)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .Must(ValidProperty).WithMessage("{PropertyName} '{PropertyValue}' is not valid");

        RuleFor(x => x.Ascending)
            .NotNull().WithMessage("{PropertyName} is required");
    }

    private bool ValidProperty(string propertyName)
    {
        var validProperties = _modelType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name.ToLower());

        return validProperties.Contains(propertyName);
    }
}