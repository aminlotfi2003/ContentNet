using ContentNet.Application.Abstractions;
using ContentNet.Application.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.PublishArticle;

public class PublishArticleCommandHandler : IRequestHandler<PublishArticleCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PublishArticleCommandHandler(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(PublishArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article", request.Id);

        article.Publish();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
