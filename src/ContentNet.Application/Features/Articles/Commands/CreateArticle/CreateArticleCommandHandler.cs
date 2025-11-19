using ContentNet.Application.Abstractions;
using ContentNet.Application.Common;
using ContentNet.Domain.Articles;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, int>
{
    private readonly IArticleRepository _articleRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateArticleCommandHandler(
        IArticleRepository articleRepository,
        ICategoryRepository categoryRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _categoryRepository = categoryRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var author = await _userRepository.GetByIdAsync(request.AuthorId, cancellationToken);
        if (author is null)
            throw new NotFoundException("User", request.AuthorId);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken);
        if (category is null)
            throw new NotFoundException("Category", request.CategoryId);

        var article = new Article(
            request.Title,
            request.Slug,
            request.Summary,
            request.Content,
            request.ContentType,
            request.AuthorId,
            request.CategoryId);

        if (request.TagIds is not null)
        {
            foreach (var tagId in request.TagIds.Distinct())
            {
                article.AddTag(tagId);
            }
        }

        await _articleRepository.AddAsync(article, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}
