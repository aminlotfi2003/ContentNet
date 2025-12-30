using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.CreateArticle;

public record CreateArticleCommand(
    string Title, 
    string Summary, 
    string Content
) : IRequest<int>;
