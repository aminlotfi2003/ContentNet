using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
