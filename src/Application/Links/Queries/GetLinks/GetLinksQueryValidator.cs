namespace LinkIo.Application.Links.Queries.GetLinks;
public class GetLinksQueryValidator : AbstractValidator<GetLinksQuery>
{
    public GetLinksQueryValidator()
    {
        RuleFor(v => v.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

        RuleFor(v => v.PageSize)
            .GreaterThanOrEqualTo(10).WithMessage("PageSize at least greater than or equal to 10.");
    }
}
