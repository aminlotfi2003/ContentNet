using ContentNet.Domain.Common;
using ContentNet.Domain.Taxonomy;
using ContentNet.Domain.Users;

namespace ContentNet.Domain.Articles;

public class Article : AuditableEntity
{
    private readonly List<ArticleTag> _articleTags = new();

    protected Article() { }

    public Article(
        string title,
        string slug,
        string summary,
        string content,
        ArticleContentType contentType,
        int authorId,
        int categoryId)
    {
        SetTitle(title);
        SetSlug(slug);
        SetSummary(summary);
        SetContent(content);

        ContentType = contentType;
        AuthorId = authorId;
        CategoryId = categoryId;

        Status = ArticleStatus.Draft;
        MarkCreated();
    }

    public string Title { get; private set; } = null!;
    public string Slug { get; private set; } = null!;
    public string Summary { get; private set; } = null!;
    public string Content { get; private set; } = null!;

    public ArticleStatus Status { get; private set; }
    public ArticleContentType ContentType { get; private set; }

    public int AuthorId { get; private set; }
    public User Author { get; private set; } = null!;

    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public DateTime? PublishedAt { get; private set; }
    public DateTime? ScheduledFor { get; private set; }

    public IReadOnlyCollection<ArticleTag> ArticleTags => _articleTags.AsReadOnly();

    // Behavior methods (invariants)

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Article title cannot be empty.");

        if (title.Length is < 5 or > 200)
            throw new DomainException("Article title length must be between 5 and 200 characters.");

        Title = title.Trim();
        MarkModified();
    }

    public void SetSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException("Article slug cannot be empty.");

        Slug = slug.Trim().ToLowerInvariant();
        MarkModified();
    }

    public void SetSummary(string summary)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new DomainException("Article summary cannot be empty.");

        if (summary.Length > 1000)
            throw new DomainException("Article summary length cannot exceed 1000 characters.");

        Summary = summary.Trim();
        MarkModified();
    }

    public void SetContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Article content cannot be empty.");

        Content = content;
        MarkModified();
    }

    public void ChangeCategory(int categoryId)
    {
        if (categoryId <= 0)
            throw new DomainException("CategoryId must be positive.");

        CategoryId = categoryId;
        MarkModified();
    }

    public void ChangeAuthor(int authorId)
    {
        if (authorId <= 0)
            throw new DomainException("AuthorId must be positive.");

        AuthorId = authorId;
        MarkModified();
    }

    public void SchedulePublication(DateTime scheduledUtc)
    {
        if (scheduledUtc <= DateTime.UtcNow)
            throw new DomainException("Scheduled publication date must be in the future.");

        Status = ArticleStatus.Scheduled;
        ScheduledFor = scheduledUtc;
        PublishedAt = null;
        MarkModified();
    }

    public void Publish(DateTime? publishUtc = null)
    {
        var publishTime = publishUtc ?? DateTime.UtcNow;

        if (Status == ArticleStatus.Archived)
            throw new DomainException("Archived article cannot be published.");

        Status = ArticleStatus.Published;
        PublishedAt = publishTime;
        ScheduledFor = null;
        MarkModified();
    }

    public void Archive()
    {
        Status = ArticleStatus.Archived;
        MarkModified();
    }

    public void AddTag(int tagId)
    {
        if (_articleTags.Any(t => t.TagId == tagId))
            return;

        _articleTags.Add(new ArticleTag(Id, tagId));
        MarkModified();
    }

    public void RemoveTag(int tagId)
    {
        var toRemove = _articleTags.FirstOrDefault(t => t.TagId == tagId);
        if (toRemove is not null)
        {
            _articleTags.Remove(toRemove);
            MarkModified();
        }
    }

    public void UpdateContent(
        string title,
        string summary,
        string content,
        ArticleContentType contentType,
        int categoryId)
    {
        SetTitle(title);
        SetSummary(summary);
        SetContent(content);
        ContentType = contentType;
        ChangeCategory(categoryId);
        MarkModified();
    }
}
