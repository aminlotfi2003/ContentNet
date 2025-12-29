using ContentNet.Domain.Enums;

namespace ContentNet.Application.Features.Articles.Dtos;

public record ArticleListItemDto(
    int Id,
    DateTimeOffset CreatedAt,
    string Title,
    ArticleStatus Status,
    DateTimeOffset? PublishedAt
);
