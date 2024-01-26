namespace ProjectSimple.Application.Helpers;

public static class TypeExtensions
{
    // Extension method to determine if a Type is numeric
    public static bool IsNumericType(this Type type)
    {
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Byte or TypeCode.SByte or TypeCode.UInt16 or
            TypeCode.UInt32 or TypeCode.UInt64 or TypeCode.Int16 or
            TypeCode.Int32 or TypeCode.Int64 or TypeCode.Decimal or
            TypeCode.Double or TypeCode.Single => true,
            _ => false,
        };
    }

    // Extension method to determine if a Type is string
    public static bool IsStringType(this Type type)
    {
        return type == typeof(string);
    }

    // Extension method to determine if a Type is bool
    public static bool IsBooleanType(this Type type)
    {
        return type == typeof(bool);
    }
}