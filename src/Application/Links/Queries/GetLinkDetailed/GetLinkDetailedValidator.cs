using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Application.Links.Queries.GetLinkDetailed;
public class GetLinkDetailedValidator : AbstractValidator<GetLinkDetailedQuery>
{
    private readonly IApplicationDbContext _context;
    private readonly IUser _user;

    public GetLinkDetailedValidator(IApplicationDbContext context, IUser user)
    {
        _context = context;
        _user = user;

        RuleFor(v => v.Id)
            .NotNull()
            .MustAsync(ExistAndBelongsToUser).WithMessage("Link with given Id does not exist or doesnt belong to user");
    }

    public async Task<bool> ExistAndBelongsToUser(int id, CancellationToken cancellationToken)
    {
        return await _context.Links.AnyAsync(l => l.Id == id && l.CreatedBy == _user.Id);
    }
}
