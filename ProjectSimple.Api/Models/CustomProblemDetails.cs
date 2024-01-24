using Microsoft.AspNetCore.Mvc;

namespace ProjectSimple.Api.Models;

public class CustomProblemDetails : ProblemDetails
{
    //public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    public List<string> Errors { get; set; } = [];
}
