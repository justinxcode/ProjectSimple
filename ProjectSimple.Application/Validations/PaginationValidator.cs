using FluentValidation;
using ProjectSimple.Application.Models;

namespace ProjectSimple.Application.Validations;

public class PaginationValidator : AbstractValidator<Pagination>
{
    private readonly Type _modelType;

    public PaginationValidator(Type modelType)
    {
        _modelType = modelType;

        RuleFor(x => x.Page)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than zero");

        RuleFor(x => x.PageSize)
            .NotNull()
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be greater than zero");

        When(x => x.Filters != null, () =>
        {
            RuleForEach(x => x.Filters).SetValidator(new FilterConditionValidator(_modelType));
        });

        When(x => x.Sorting != null, () =>
        {
            RuleForEach(x => x.Sorting).SetValidator(new SortConditionValidator(_modelType));
        });
    }
}