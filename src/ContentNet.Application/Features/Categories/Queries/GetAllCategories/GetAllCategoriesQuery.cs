using ContentNet.Application.DTOs;
using MediatR;

namespace ContentNet.Application.Features.Categories.Queries.GetAllCategories;

public class GetAllCategoriesQuery : IRequest<List<CategoryDto>>
{
}
