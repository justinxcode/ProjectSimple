using ProjectSimple.Domain.Models.Common;

namespace ProjectSimple.Domain.Models;

public class User : AuditEntity
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
