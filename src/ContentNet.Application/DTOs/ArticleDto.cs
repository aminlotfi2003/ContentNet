using ContentNet.Domain.Articles;

namespace ContentNet.Application.DTOs;

public class ArticleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public ArticleStatus Status { get; set; }
    public ArticleContentType ContentType { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public int AuthorId { get; set; }
    public string AuthorUserName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? PublishedAt { get; set; }
    public IReadOnlyList<string> Tags { get; set; } = Array.Empty<string>();
}
