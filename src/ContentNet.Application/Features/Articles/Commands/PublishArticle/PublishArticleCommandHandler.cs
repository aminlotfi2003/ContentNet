using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using ContentNet.Domain.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.PublishArticle;

public class PublishArticleCommandHandler(IArticleRepository repo, IUnitOfWork uow, IDateTimeProvider clock) : IRequestHandler<PublishArticleCommand, Unit>
{
    private readonly IDateTimeProvider _clock = clock;
    private readonly IArticleRepository _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article was not found.");

        article.Publish(_clock);

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
