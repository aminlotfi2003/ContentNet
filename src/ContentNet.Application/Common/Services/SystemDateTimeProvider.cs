using ContentNet.Domain.Common;

namespace ContentNet.Application.Common.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}
