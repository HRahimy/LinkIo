﻿namespace LinkIo.Domain.Entities;
public class Link : BaseAuditableEntity
{
    public required string OriginalUrl { get; set; }
    public string? ShortUrlCode { get; set; }

    public IList<LinkReferrer> Referrers { get; private set; } = new List<LinkReferrer>();
}
