using ContentNet.Application.Features.Articles.Dtos;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticles;

public record GetArticlesQuery : IRequest<IReadOnlyList<ArticleListItemDto>>;
