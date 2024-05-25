using LinkIo.Application.Common.Interfaces;
using LinkIo.Application.Common.Security;
using LinkIo.Domain.Constants;
using LinkIo.Domain.Events;

namespace LinkIo.Application.Links.Commands.EditLink;

[Authorize(PermissionScope = Permissions.EditLinks)]
public record EditLinkCommand : IRequest
{
    public int Id { get; init; }
    public required string ShortUrlCode { get; init; }
}

public class EditLinkCommandHandler : IRequestHandler<EditLinkCommand>
{
    private readonly IApplicationDbContext _context;

    public EditLinkCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(EditLinkCommand request, CancellationToken cancellationToken)
    {
        var link = await _context.Links.FirstAsync(e => e.Id == request.Id);

        link.ShortUrlCode = request.ShortUrlCode;

        link.AddDomainEvent(new LinkEditedEvent(link));

        _context.Links.Update(link);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
