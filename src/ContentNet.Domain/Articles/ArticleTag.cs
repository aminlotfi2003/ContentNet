using ContentNet.Domain.Taxonomy;

namespace ContentNet.Domain.Articles;

public class ArticleTag
{
    protected ArticleTag() { }

    public ArticleTag(int articleId, int tagId)
    {
        ArticleId = articleId;
        TagId = tagId;
    }

    public int ArticleId { get; private set; }
    public int TagId { get; private set; }

    public Article Article { get; private set; } = null!;
    public Tag Tag { get; private set; } = null!;
}
