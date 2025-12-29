using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.DeleteArticle;

public record DeleteArticleCommand(int Id) : IRequest<Unit>;
