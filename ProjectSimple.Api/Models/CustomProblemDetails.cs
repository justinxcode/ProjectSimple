using Microsoft.AspNetCore.Mvc;

namespace ProjectSimple.Api.Models;

public class CustomProblemDetails : ProblemDetails
{
    public List<string> ErrorsList { get; set; } = [];
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}
