namespace LinkIo.Domain.Entities;
public class LinkReferrer : BaseAuditableEntity
{
    public required string Url { get; set; }
    public int LinkId { get; set; }
    public Link Link { get; set; } = null!;
}
