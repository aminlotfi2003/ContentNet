using ContentNet.Domain.Entities;

namespace ContentNet.Application.Common.Abstractions.Services;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}
