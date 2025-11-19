using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public int? ParentCategoryId { get; set; }
}
