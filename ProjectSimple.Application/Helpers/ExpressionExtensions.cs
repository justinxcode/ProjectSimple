using System.Linq.Expressions;

namespace ProjectSimple.Application.Helpers;

public class ExpressionExtensions
{
    public static Expression GetStringComparisonExpression(string methodName, Expression property, Expression constant)
    {
        var method = typeof(string).GetMethod(methodName, [typeof(string)]);
        return Expression.Call(property, method, constant);
    }
}