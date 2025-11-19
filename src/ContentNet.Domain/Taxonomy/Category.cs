using ContentNet.Domain.Articles;
using ContentNet.Domain.Common;

namespace ContentNet.Domain.Taxonomy;

public class Category : AuditableEntity
{
    private readonly List<Article> _articles = new();

    protected Category() { }

    public Category(string name, string slug, string? description = null, int? parentCategoryId = null)
    {
        SetName(name);
        SetSlug(slug);
        Description = description;
        ParentCategoryId = parentCategoryId;
        MarkCreated();
    }

    public string Name { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string? Description { get; private set; }

    public int? ParentCategoryId { get; private set; }
    public Category? ParentCategory { get; private set; }

    public IReadOnlyCollection<Article> Articles => _articles.AsReadOnly();

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        if (name.Length is < 3 or > 100)
            throw new DomainException("Category name length must be between 3 and 100 characters.");

        Name = name.Trim();
        MarkModified();
    }

    public void SetSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException("Category slug cannot be empty.");

        Slug = slug.Trim().ToLowerInvariant();
        MarkModified();
    }

    public void SetDescription(string? description)
    {
        Description = description;
        MarkModified();
    }

    public void SetParentCategory(int? parentCategoryId)
    {
        if (parentCategoryId == Id)
            throw new DomainException("Category cannot be parent of itself.");

        ParentCategoryId = parentCategoryId;
        MarkModified();
    }

    internal void AddArticle(Article article)
    {
        _articles.Add(article);
    }
}
