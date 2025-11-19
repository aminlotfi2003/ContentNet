using FluentValidation;

namespace ContentNet.Application.Features.Articles.Commands.ScheduleArticle;

public class ScheduleArticleCommandValidator : AbstractValidator<ScheduleArticleCommand>
{
    public ScheduleArticleCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.ScheduledForUtc)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Scheduled time must be in the future.");
    }
}
