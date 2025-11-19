using ContentNet.Application.Abstractions;
using ContentNet.Application.DTOs;
using ContentNet.Domain.Taxonomy;
using MediatR;

namespace ContentNet.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var exists = await _tagRepository.SlugExistsAsync(request.Slug, null, cancellationToken);
        if (exists)
            throw new ApplicationException($"Tag slug '{request.Slug}' already exists.");

        var tag = new Tag(request.Name, request.Slug);

        await _tagRepository.AddAsync(tag, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name,
            Slug = tag.Slug
        };
    }
}
