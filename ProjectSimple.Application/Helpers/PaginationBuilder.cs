using ProjectSimple.Application.Models;
using System.Linq.Expressions;

namespace ProjectSimple.Application.Helpers;

public class PaginationBuilder
{
    public static Expression<Func<T, bool>> BuildFilters<T>(List<FilterCondition> conditions)
    {
        if (conditions.Count != 0)
        {
            Expression? expression = null;

            var parameter = Expression.Parameter(typeof(T), "x");

            foreach (var condition in conditions)
            {
                // Get property and value
                var property = Expression.Property(parameter, condition.Property.ToLower());
                var propertyType = property.Type;
                var value = Convert.ChangeType(condition.Value, propertyType);
                var constant = Expression.Constant(value, propertyType);

                // Do comparison logic
                Expression comparison;
                switch (condition.Operator.ToLower())
                {
                    case "eq":
                        comparison = Expression.Equal(property, constant);
                        break;
                    case "neq":
                        comparison = Expression.NotEqual(property, constant);
                        break;
                    case "gt":
                        comparison = Expression.GreaterThan(property, constant);
                        break;
                    case "lt":
                        comparison = Expression.LessThan(property, constant);
                        break;
                    case "gte":
                        comparison = Expression.GreaterThanOrEqual(property, constant);
                        break;
                    case "lte":
                        comparison = Expression.LessThanOrEqual(property, constant);
                        break;
                    case "contains":
                    case "startswith":
                    case "endswith":
                        string methodName = condition.Operator switch
                        {
                            "contains" => "Contains",
                            "startswith" => "StartsWith",
                            "endswith" => "EndsWith",
                            _ => throw new InvalidOperationException("Invalid string comparison operator")
                        };
                        comparison = ExpressionExtensions.GetStringComparisonExpression(methodName, property, constant);
                        break;
                    // Add more cases as necessary
                    default:
                        throw new InvalidOperationException("Invalid operator");
                }

                expression = expression == null ? comparison : Expression.AndAlso(expression, comparison);
            }

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }
        else
        {
            return x => true;
        }
    }

    public static IOrderedQueryable<T> ApplySorting<T>(IQueryable<T> query, List<SortCondition> sortConditions)
    {
        if (sortConditions.Count != 0)
        {
            IOrderedQueryable<T>? orderedQuery = null;

            for (int i = 0; i < sortConditions.Count; i++)
            {
                var condition = sortConditions[i];
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Property(parameter, condition.Property.ToLower());
                var lambda = Expression.Lambda(property, parameter);

                string methodName;
                if (i == 0)
                {
                    methodName = condition.Ascending ? "OrderBy" : "OrderByDescending";
                }
                else
                {
                    methodName = condition.Ascending ? "ThenBy" : "ThenByDescending";
                }

                var resultExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    [typeof(T), property.Type],
                    query.Expression,
                    Expression.Quote(lambda));

                query = query.Provider.CreateQuery<T>(resultExpression);
                orderedQuery = (IOrderedQueryable<T>)query;
            }

            return orderedQuery;
        }
        else
        {
            return (IOrderedQueryable<T>)query;
        }
    }
}