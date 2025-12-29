using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.DeleteArticle;

public class DeleteArticleCommandHandler(IArticleRepository repo, IUnitOfWork uow) : IRequestHandler<DeleteArticleCommand, Unit>
{
    private readonly IArticleRepository _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<Unit> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repo.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article was not found.");

        _repo.Remove(article);
        await _uow.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
