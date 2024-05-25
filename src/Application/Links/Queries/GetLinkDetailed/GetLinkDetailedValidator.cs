using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Application.Links.Queries.GetLinkDetailed;
public class GetLinkDetailedValidator : AbstractValidator<GetLinkDetailedQuery>
{
    private readonly IApplicationDbContext _context;

    public GetLinkDetailedValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Id)
            .NotNull()
            .MustAsync(ExistWithId).WithMessage("Link with given Id does not exist");
    }

    public async Task<bool> ExistWithId(int id, CancellationToken cancellationToken)
    {
        return await _context.Links.AnyAsync(l => l.Id == id);
    }
}
