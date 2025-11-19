using ContentNet.Domain.Common;

namespace ContentNet.Domain.Users;

public class Role : Entity
{
    private readonly List<UserRole> _userRoles = new();

    protected Role() { }

    public Role(string name, string? description = null)
    {
        SetName(name);
        Description = description;
    }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Role name cannot be empty.");

        if (name.Length is < 3 or > 50)
            throw new DomainException("Role name length must be between 3 and 50 characters.");

        Name = name.Trim();
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }
}
