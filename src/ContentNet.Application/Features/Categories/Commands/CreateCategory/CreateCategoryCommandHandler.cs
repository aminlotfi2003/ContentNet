using ContentNet.Application.Abstractions;
using ContentNet.Application.DTOs;
using ContentNet.Domain.Taxonomy;
using MediatR;

namespace ContentNet.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.SlugExistsAsync(request.Slug, null, cancellationToken);
        if (exists)
            throw new ApplicationException($"Category slug '{request.Slug}' already exists.");

        var category = new Category(
            request.Name,
            request.Slug,
            request.Description,
            request.ParentCategoryId);

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId
        };
    }
}
