using ContentNet.Application.Features.Articles.Dtos;
using MediatR;

namespace ContentNet.Application.Features.Articles.Queries.GetArticleDetails;

public record GetArticleDetailsQuery(int Id) : IRequest<ArticleDto>;
