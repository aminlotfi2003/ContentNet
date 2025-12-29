using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using ContentNet.Domain.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.ScheduleArticle;

public class ScheduleArticleCommandHandler(IArticleRepository repo, IUnitOfWork uow, IDateTimeProvider clock) : IRequestHandler<ScheduleArticleCommand, Unit>
{
    private readonly IDateTimeProvider _clock = clock;
    private readonly IArticleRepository _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(ScheduleArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article was not found.");

        article.SchedulePublication(request.ScheduledAtUtc, _clock);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
