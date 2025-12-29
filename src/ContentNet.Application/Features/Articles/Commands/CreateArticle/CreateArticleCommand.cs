using ContentNet.Domain.Enums;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.CreateArticle;

public record CreateArticleCommand(
    string Title, 
    string Summary, 
    string Content,
    ArticleStatus Status,
    DateTimeOffset? PublishedAt,
    DateTimeOffset? ScheduledAt
) : IRequest<int>;
