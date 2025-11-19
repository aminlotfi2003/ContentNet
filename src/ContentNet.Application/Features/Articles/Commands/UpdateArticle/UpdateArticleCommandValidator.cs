using FluentValidation;

namespace ContentNet.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    public UpdateArticleCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(5).MaximumLength(200);
        RuleFor(x => x.Slug)
            .NotEmpty()
            .Matches("^[a-z0-9-]+$");
        RuleFor(x => x.Summary)
            .NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Content)
            .NotEmpty();
        RuleFor(x => x.CategoryId)
            .GreaterThan(0);
    }
}
