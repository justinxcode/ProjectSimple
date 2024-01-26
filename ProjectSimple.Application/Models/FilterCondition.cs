namespace ProjectSimple.Application.Models;

public class FilterCondition
{
    public string Property { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Operator { get; set; } = string.Empty;
}