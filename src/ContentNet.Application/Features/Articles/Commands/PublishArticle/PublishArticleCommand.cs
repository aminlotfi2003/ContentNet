using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.PublishArticle;

public record PublishArticleCommand(int Id) : IRequest<Unit>;
