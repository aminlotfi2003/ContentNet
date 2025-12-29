using AutoMapper;
using ContentNet.Application.Features.Articles.Dtos;
using ContentNet.Domain.Entities;

namespace ContentNet.Application.Common.Mappings;

public class ArticleProfile : Profile
{
    public ArticleProfile()
    {
        CreateMap<Article, ArticleDto>();

        CreateMap<Article, ArticleListItemDto>();
    }
}
