using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.PublishArticle;

public class PublishArticleCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
