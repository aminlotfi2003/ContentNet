namespace ContentNet.Domain.Common;

public interface IDateTimeProvider
{
    DateTimeOffset UtcNow { get; }
}
