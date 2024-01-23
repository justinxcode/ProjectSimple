namespace ProjectSimple.Domain.Models.Common;

public abstract class AuditEntity : BaseEntity
{
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
