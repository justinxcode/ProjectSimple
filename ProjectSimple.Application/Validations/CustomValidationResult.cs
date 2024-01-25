namespace ProjectSimple.Application.Validations;

public class CustomValidationResult
{
    public bool IsValid => ErrorsList.Count == 0;
    public List<string> ErrorsList { get; set; } = [];
}