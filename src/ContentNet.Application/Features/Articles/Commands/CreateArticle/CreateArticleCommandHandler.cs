using ContentNet.Application.Common.Abstractions.Persistence;
using ContentNet.Application.Common.Exceptions;
using ContentNet.Domain.Entities;
using MediatR;

namespace ContentNet.Application.Features.Articles.Commands.CreateArticle;

public class CreateArticleCommandHandler(IArticleRepository repo, IUnitOfWork uow) : IRequestHandler<CreateArticleCommand, int>
{
    private readonly IArticleRepository _repo = repo;
    private readonly IUnitOfWork _uow = uow;

    public async Task<int> Handle(CreateArticleCommand request, CancellationToken cancellationToken = default)
    {
        var titleExists = await _repo.AnyAsync(x => x.Title == request.Title, cancellationToken);
        if (titleExists)
            throw new ConflictException($"A article with title '{request.Title}' already exists.");

        var article = Article.Create(
            request.Title,
            request.Summary,
            request.Content,
            request.Status,
            request.PublishedAt,
            request.ScheduledAt
        );

        await _repo.AddAsync(article, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}
