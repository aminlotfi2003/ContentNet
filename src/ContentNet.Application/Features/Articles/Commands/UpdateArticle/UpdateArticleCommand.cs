using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.UpdateArticle;

public record UpdateArticleCommand(
    int Id,
    string Title,
    string Summary,
    string Content
) : IRequest<Unit>;
