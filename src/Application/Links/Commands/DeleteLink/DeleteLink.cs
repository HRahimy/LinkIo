using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Security;
using LinkIo.Domain.Constants;
using LinkIo.Domain.Events;

namespace LinkIo.Application.Links.Commands.DeleteLink;

[Authorize(PermissionScope = Permissions.DeleteLinks)]
public record DeleteLinkCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteLinkCommandHandler : IRequestHandler<DeleteLinkCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteLinkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteLinkCommand request, CancellationToken cancellationToken)
    {
        var link = await _context.Links.FirstAsync(e => e.Id == request.Id, cancellationToken);

        link.AddDomainEvent(new LinkDeletedEvent(link));

        _context.Links.Remove(link);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
