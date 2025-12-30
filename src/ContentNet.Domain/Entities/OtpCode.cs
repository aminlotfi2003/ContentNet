using ContentNet.Domain.Common;

namespace ContentNet.Domain.Entities;

public class OtpCode : IEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsUsed { get; set; }
}
