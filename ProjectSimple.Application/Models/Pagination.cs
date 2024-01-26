namespace ProjectSimple.Application.Models;

public class Pagination
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public List<FilterCondition> Filters { get; set; } = [];
    public List<SortCondition> Sorting { get; set; } = [];
}