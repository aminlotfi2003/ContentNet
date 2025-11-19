using ContentNet.Domain.Articles;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommand : IRequest<int>
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public ArticleContentType ContentType { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public List<int>? TagIds { get; set; }
}
