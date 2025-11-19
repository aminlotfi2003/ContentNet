using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.ScheduleArticle;

public class ScheduleArticleCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public DateTime ScheduledForUtc { get; set; }
}
