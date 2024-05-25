using LinkIo.Application.Common.Interfaces;

namespace LinkIo.Application.Links.Queries.GetRedirectUrl;
public class GetRedirectUrlQueryValidator : AbstractValidator<GetRedirectUrlQuery>
{
    private readonly IApplicationDbContext _context;

    public GetRedirectUrlQueryValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.ShortUrlCode)
            .NotNull()
            .NotEmpty()
            .Length(15).WithMessage("ShortUrlCode must be exactly 15 characters")
            .MustAsync(ExistWithCode).WithMessage("No Link with given ShortUrlCode exists");
    }

    public async Task<bool> ExistWithCode(string code, CancellationToken cancellationToken)
    {
        return await _context.Links.AnyAsync(e => e.ShortUrlCode == code, cancellationToken);
    }
}
