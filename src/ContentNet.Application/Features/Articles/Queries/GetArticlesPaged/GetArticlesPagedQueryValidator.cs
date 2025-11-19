using FluentValidation;

namespace ContentNet.Application.Features.Articles.Queries.GetArticlesPaged;

public class GetArticlesPagedQueryValidator : AbstractValidator<GetArticlesPagedQuery>
{
    public GetArticlesPagedQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
    }
}
