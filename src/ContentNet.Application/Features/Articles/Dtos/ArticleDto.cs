using ContentNet.Domain.Enums;

namespace ContentNet.Application.Features.Articles.Dtos;

public record ArticleDto(
    int Id,
    DateTimeOffset CreatedAt,
    DateTimeOffset ModifiedAt,
    string Title,
    string Summary,
    string Content,
    ArticleStatus Status,
    DateTimeOffset? PublishedAt,
    DateTimeOffset? ScheduledAt
);
