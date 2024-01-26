namespace ProjectSimple.Application.Models;

public class SortCondition
{
    public string Property { get; set; } = string.Empty;

    public bool Ascending { get; set; } = true; // Default to ascending sort
}