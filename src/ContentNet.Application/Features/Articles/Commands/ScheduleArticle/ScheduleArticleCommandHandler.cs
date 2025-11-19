using ContentNet.Application.Abstractions;
using ContentNet.Application.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.ScheduleArticle;

public class ScheduleArticleCommandHandler : IRequestHandler<ScheduleArticleCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleArticleCommandHandler(
        IArticleRepository articleRepository,
        IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ScheduleArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article", request.Id);

        article.SchedulePublication(request.ScheduledForUtc);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
