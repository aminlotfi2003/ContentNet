namespace ContentNet.Domain.Common;

public abstract class EntityBase : IEntity, IAuditableEntity
{
    public int Id { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ModifiedAt { get; set; }

    public void MarkCreated(IDateTimeProvider clock)
    {
        CreatedAt = clock.UtcNow;
    }

    public void MarkModified(IDateTimeProvider clock)
    {
        ModifiedAt = clock.UtcNow;
    }
}
