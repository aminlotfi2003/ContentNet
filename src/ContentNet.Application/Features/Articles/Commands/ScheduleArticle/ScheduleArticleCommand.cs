using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.ScheduleArticle;

public record ScheduleArticleCommand(int Id, DateTimeOffset ScheduledAtUtc) : IRequest<Unit>;
