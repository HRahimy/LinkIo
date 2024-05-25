using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Mappings;
using LinkIo.Application.Common.Models;
using LinkIo.Application.Common.Security;
using LinkIo.Application.Links.Models;
using LinkIo.Domain.Constants;

namespace LinkIo.Application.Links.Queries.GetLinks;
[Authorize(PermissionScope = Permissions.ReadLinks)]
public record GetLinksQuery : IRequest<PaginatedList<LinkDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetLinksQueryHandler : IRequestHandler<GetLinksQuery, PaginatedList<LinkDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLinksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LinkDto>> Handle(GetLinksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Links
            .ProjectTo<LinkDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
