namespace ContentNet.Domain.Common;

public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; }
    DateTimeOffset? ModifiedAt { get; }
}
