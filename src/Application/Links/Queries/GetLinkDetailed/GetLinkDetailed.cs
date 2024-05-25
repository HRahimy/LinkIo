using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Security;
using LinkIo.Application.Links.Models;
using LinkIo.Domain.Constants;
using Microsoft.Identity.Client;

namespace LinkIo.Application.Links.Queries.GetLinkDetailed;

[Authorize(PermissionScope = Permissions.ReadLinks)]
public record GetLinkDetailedQuery : IRequest<LinkDetailsDto>
{
    public int Id { get; init; }
}

public class GetLinkDetailedQueryHandler : IRequestHandler<GetLinkDetailedQuery, LinkDetailsDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLinkDetailedQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LinkDetailsDto> Handle(GetLinkDetailedQuery request, CancellationToken cancellationToken)
    {
        var link = await _context.Links
            .ProjectTo<LinkDetailsDto>(_mapper.ConfigurationProvider)
            .FirstAsync(e => e.Id == request.Id);

        var referrerCounts = _context.LinkReferrers
            .Where(e => e.LinkId == link.Id)
            .GroupBy(x => x.Url)
            .Select(g => new
            {
                Value = g.Key,
                Count = g.Count()
            });

        var dto = _mapper.Map<LinkDetailsDto>(link);

        var refs = referrerCounts.Select(x => new LinkReferrerDto
        {
            Url = x.Value,
            Count = x.Count
        }).ToList();

        dto.Referrers = refs;

        return dto;
    }
}
