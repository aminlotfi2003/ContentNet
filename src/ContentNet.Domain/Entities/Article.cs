using ContentNet.Domain.Common;
using ContentNet.Domain.Enums;

namespace ContentNet.Domain.Entities;

public class Article : EntityBase
{
    public string Title { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTimeOffset? PublishedAt { get; set; }
    public DateTimeOffset? ScheduledAt { get; set; }

    private Article()
    {
    }

    public static Article Create(
        string title,
        string summary,
        string content,
        ArticleStatus status,
        DateTimeOffset? publishedAt = null,
        DateTimeOffset? scheduledAt = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Article title cannot be empty.");

        if (title.Length is < 5 or > 200)
            throw new DomainException("Article title length must be between 5 and 200 characters.");

        if (string.IsNullOrWhiteSpace(summary))
            throw new DomainException("Article summary cannot be empty.");

        if (summary.Length > 1000)
            throw new DomainException("Article summary length cannot exceed 1000 characters.");

        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Article content cannot be empty.");

        return new Article
        {
            Title = title,
            Summary = summary,
            Content = content,
            Status = status,
            PublishedAt = publishedAt,
            ScheduledAt = scheduledAt
        };
    }

    public void SetTitle(string title, IDateTimeProvider clock)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Article title cannot be empty.");

        if (title.Length is < 5 or > 200)
            throw new DomainException("Article title length must be between 5 and 200 characters.");

        Title = title.Trim();
        MarkModified(clock);
    }

    public void SetSummary(string summary, IDateTimeProvider clock)
    {
        if (string.IsNullOrWhiteSpace(summary))
            throw new DomainException("Article summary cannot be empty.");

        if (summary.Length > 1000)
            throw new DomainException("Article summary length cannot exceed 1000 characters.");

        Summary = summary.Trim();
        MarkModified(clock);
    }

    public void SetContent(string content, IDateTimeProvider clock)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Article content cannot be empty.");

        Content = content;
        MarkModified(clock);
    }

    public void SchedulePublication(DateTimeOffset scheduledAtUtc, IDateTimeProvider clock)
    {
        if (scheduledAtUtc <= clock.UtcNow)
            throw new DomainException("Scheduled publication date must be in the future.");

        if (Status == ArticleStatus.Archived)
            throw new DomainException("Archived article cannot be scheduled.");

        if (Status == ArticleStatus.Published)
            throw new DomainException("Published article cannot be scheduled.");

        if (Status == ArticleStatus.Scheduled && ScheduledAt == scheduledAtUtc)
            return;

        Status = ArticleStatus.Scheduled;
        ScheduledAt = scheduledAtUtc;
        PublishedAt = null;
        MarkModified(clock);
    }

    public void Publish(IDateTimeProvider clock)
    {
        if (Status == ArticleStatus.Archived)
            throw new DomainException("Archived article cannot be published.");

        Status = ArticleStatus.Published;
        PublishedAt = clock.UtcNow;
        ScheduledAt = null;
        MarkModified(clock);
    }

    public void Archive(IDateTimeProvider clock)
    {
        Status = ArticleStatus.Archived;
        MarkModified(clock);
    }

    public void UpdateArticle(string title, string summary, string content, IDateTimeProvider clock)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Article title cannot be empty.");

        if (title.Length is < 5 or > 200)
            throw new DomainException("Article title length must be between 5 and 200 characters.");

        if (string.IsNullOrWhiteSpace(summary))
            throw new DomainException("Article summary cannot be empty.");

        if (summary.Length > 1000)
            throw new DomainException("Article summary length cannot exceed 1000 characters.");

        if (string.IsNullOrWhiteSpace(content))
            throw new DomainException("Article content cannot be empty.");

        Title = title.Trim();
        Summary = summary.Trim();
        Content = content;
        MarkModified(clock);
    }
}
