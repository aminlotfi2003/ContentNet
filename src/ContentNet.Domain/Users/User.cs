using ContentNet.Domain.Articles;
using ContentNet.Domain.Common;

namespace ContentNet.Domain.Users;

public class User : AuditableEntity
{
    private readonly List<UserRole> _userRoles = new();
    private readonly List<Article> _articles = new();

    protected User() { }

    public User(string userName, string email)
    {
        SetUserName(userName);
        SetEmail(email);
        IsActive = true;
        MarkCreated();
    }

    public string UserName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();
    public IReadOnlyCollection<Article> Articles => _articles.AsReadOnly();

    public void SetUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new DomainException("User name cannot be empty.");

        if (userName.Length is < 3 or > 50)
            throw new DomainException("User name length must be between 3 and 50 characters.");

        UserName = userName.Trim();
        MarkModified();
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email cannot be empty.");

        if (!email.Contains('@'))
            throw new DomainException("Email format is not valid.");

        Email = email.Trim();
        MarkModified();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkModified();
    }

    public void Activate()
    {
        IsActive = true;
        MarkModified();
    }

    internal void AddArticle(Article article)
    {
        _articles.Add(article);
    }
}
