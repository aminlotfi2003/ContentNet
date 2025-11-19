using ContentNet.Domain.Articles;
using ContentNet.Domain.Common;

namespace ContentNet.Domain.Taxonomy;

public class Tag : AuditableEntity
{
    private readonly List<ArticleTag> _articleTags = new();

    protected Tag() { }

    public Tag(string name, string slug)
    {
        SetName(name);
        SetSlug(slug);
        MarkCreated();
    }

    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;

    public IReadOnlyCollection<ArticleTag> ArticleTags => _articleTags.AsReadOnly();

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Tag name cannot be empty.");

        if (name.Length is < 2 or > 50)
            throw new DomainException("Tag name length must be between 2 and 50 characters.");

        Name = name.Trim();
        MarkModified();
    }

    public void SetSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException("Tag slug cannot be empty.");

        Slug = slug.Trim().ToLowerInvariant();
        MarkModified();
    }
}
