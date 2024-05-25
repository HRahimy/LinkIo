using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Application.Links.Commands.DeleteLink;
public class DeleteLinkCommandValidator : AbstractValidator<DeleteLinkCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLinkCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotNull()
            .MustAsync(ExistWithId).WithMessage("Could not find Link with given Id");
    }

    public async Task<bool> ExistWithId(int id, CancellationToken cancellationToken)
    {
        return await _context.Links.AnyAsync(l => l.Id == id, cancellationToken);
    }
}
