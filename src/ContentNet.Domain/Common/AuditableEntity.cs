namespace ContentNet.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
    public DateTime? LastModifiedAt { get; protected set; }

    public void MarkCreated()
    {
        CreatedAt = DateTime.UtcNow;
    }

    public void MarkModified()
    {
        LastModifiedAt = DateTime.UtcNow;
    }
}
