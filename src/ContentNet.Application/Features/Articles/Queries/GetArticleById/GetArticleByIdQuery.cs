using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticleById;

public class GetArticleByIdQuery : IRequest<ArticleDto?>
{
    public int Id { get; set; }

    public GetArticleByIdQuery(int id)
    {
        Id = id;
    }
}
