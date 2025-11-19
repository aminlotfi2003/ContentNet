using ContentNet.Domain.Articles;

namespace ContentNet.Application.DTOs;

public class ArticleListItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public ArticleStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
}
