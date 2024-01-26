using FluentValidation;
using ProjectSimple.Application.Helpers;
using ProjectSimple.Application.Models;
using System.Reflection;
using System.Text.Json;

namespace ProjectSimple.Application.Validations;

public class FilterConditionValidator : AbstractValidator<FilterCondition>
{
    private readonly Type _modelType;

    public FilterConditionValidator(Type modelType)
    {
        _modelType = modelType;

        RuleFor(x => x.Property)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .Must(ValidProperty).WithMessage("{PropertyName} '{PropertyValue}' is not valid");

        RuleFor(x => x.Value)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(x => x.Operator)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .Must(ValidOperator).WithMessage("{PropertyName} '{PropertyValue}' is not valid");

        RuleFor(x => x)
            .Must(ValidValueForProperty)
            .WithMessage(x => $"The value '{x.Value}' is not valid for the property '{x.Property}'")
            .Must(ValidOperatorForProperty)
            .WithMessage(x => $"The operator '{x.Operator}' is not valid for the property '{x.Property}'");

    }

    private bool ValidProperty(string propertyName)
    {
        var validProperties = _modelType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => p.Name.ToLower());

        return validProperties.Contains(propertyName);
    }

    private bool ValidOperator(string operatorValue)
    {
        var validOperators = new[] { "eq", "neq",  "gt", "lt", "gte", "lte", "contains", "startswith", "endswith" };

        return validOperators.Contains(operatorValue);
    }

    private bool ValidValueForProperty(FilterCondition condition)
    {
        var propertyInfo = _modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .First(p => string.Equals(p.Name, condition.Property, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
        {
            return false; // Property does not exist in the model
        }

        try
        {
            // Attempt to convert the value to the property type
            var convertedValue = Convert.ChangeType(condition.Value, propertyInfo.PropertyType);

            return true;
        }
        catch
        {
            // Conversion failed
            return false;
        }
    }

    private bool ValidOperatorForProperty(FilterCondition condition)
    {
        //var propertyInfo = _modelType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        var propertyInfo = _modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .First(p => string.Equals(p.Name, condition.Property, StringComparison.OrdinalIgnoreCase));

        if (propertyInfo == null)
        {
            return false; // Property does not exist
        }

        var validOperators = new List<string>(); // Operators valid for all types

        // Add operators based on property type
        if (propertyInfo.PropertyType.IsBooleanType())
        {
            validOperators.AddRange(new[] { "eq", "neq" });
        }
        else if (propertyInfo.PropertyType.IsNumericType())
        {
            validOperators.AddRange(new[] { "gt", "lt", "gte", "lte" });
        }
        else if (propertyInfo.PropertyType.IsStringType())
        {
            validOperators.AddRange(new[] { "contains", "startswith", "endswith" });
        }

        return validOperators.Contains(condition.Operator.ToLower());
    }

}