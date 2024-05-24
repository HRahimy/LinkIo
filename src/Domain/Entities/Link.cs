namespace LinkIo.Domain.Entities;
public class Link : BaseAuditableEntity
{
    public required string OriginalUrl { get; set; }
    public required string ShortUrl { get; set; }
    public int ClickCount { get; set; }

    public IList<LinkReferrer> Referrers { get; private set; } = new List<LinkReferrer>();
}
