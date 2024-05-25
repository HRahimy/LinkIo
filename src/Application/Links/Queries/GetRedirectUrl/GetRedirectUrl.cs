﻿
using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Security;
using LinkIo.Domain.Entities;
using LinkIo.Domain.Events;

namespace LinkIo.Application.Links.Queries.GetRedirectUrl;

[Authorize(Scope = "read:links")]
public record GetRedirectUrlQuery : IRequest<string>
{
    public required string ShortUrlCode { get; init; }
    public string? ReferrerUrl { get; init; }
}

public class GetRedirectUrlQueryHandler : IRequestHandler<GetRedirectUrlQuery, string>
{
    private readonly IApplicationDbContext _context;
    public GetRedirectUrlQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(GetRedirectUrlQuery request, CancellationToken cancellationToken)
    {
        var linkEntity = await _context.Links
            .FirstAsync(e => e.ShortUrlCode == request.ShortUrlCode, cancellationToken);

        if (linkEntity == null)
        {
            throw new NotFoundException(request.ShortUrlCode, typeof(Link).Name);
        }

        var referrerEntity = new LinkReferrer
        {
            Link = linkEntity,
            Url = request.ReferrerUrl ?? string.Empty,
        };

        linkEntity.ClickCount += 1;

        _context.Links.Update(linkEntity);
        _context.LinkReferrers.Add(referrerEntity);

        referrerEntity.AddDomainEvent(new LinkOpenedEvent(linkEntity, referrerEntity));

        await _context.SaveChangesAsync(cancellationToken);

        return linkEntity.OriginalUrl;
    }
}
