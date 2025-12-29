using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using ContentNet.Domain.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandHandler(IDateTimeProvider clock, IArticleRepository repo, IUnitOfWork uow) : IRequestHandler<UpdateArticleCommand, Unit>
{
    private readonly IDateTimeProvider _clock = clock;
    private readonly IArticleRepository _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article was not found.");

        article.UpdateArticle(
            request.Title,
            request.Summary,
            request.Content,
            _clock
        );

        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
