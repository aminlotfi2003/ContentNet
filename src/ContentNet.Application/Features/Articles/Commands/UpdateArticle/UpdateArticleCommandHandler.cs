using ContentNet.Application.Abstractions;
using ContentNet.Application.Common;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.UpdateArticle;

public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, Unit>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArticleCommandHandler(
        IArticleRepository articleRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _articleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (article is null)
            throw new NotFoundException("Article", request.Id);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            throw new NotFoundException("Category", request.CategoryId);

        article.UpdateContent(
            request.Title,
            request.Summary,
            request.Content,
            request.ContentType,
            request.CategoryId);

        if (request.TagIds is not null)
        {
            foreach (var tag in article.ArticleTags.ToList())
            {
                article.RemoveTag(tag.TagId);
            }

            foreach (var tagId in request.TagIds.Distinct())
            {
                article.AddTag(tagId);
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
