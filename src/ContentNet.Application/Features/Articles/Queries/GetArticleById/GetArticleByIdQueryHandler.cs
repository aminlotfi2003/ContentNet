using ContentNet.Application.Abstractions;
using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticleById;

public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto?>
{
    private readonly IArticleRepository _articleRepository;

    public GetArticleByIdQueryHandler(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<ArticleDto?> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            return null;

        var dto = new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Slug = article.Slug,
            Summary = article.Summary,
            Content = article.Content,
            Status = article.Status,
            ContentType = article.ContentType,
            CategoryId = article.CategoryId,
            CategoryName = article.Category?.Name ?? string.Empty,
            AuthorId = article.AuthorId,
            AuthorUserName = article.Author?.UserName ?? string.Empty,
            CreatedAt = article.CreatedAt,
            PublishedAt = article.PublishedAt,
            Tags = article.ArticleTags
                .Select(at => at.Tag?.Name ?? string.Empty)
                .Where(n => !string.IsNullOrEmpty(n))
                .ToList()
        };

        return dto;
    }
}
