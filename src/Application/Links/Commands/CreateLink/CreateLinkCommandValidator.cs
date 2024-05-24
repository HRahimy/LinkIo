namespace LinkIo.Application.Links.Commands.CreateLink;
public class CreateLinkCommandValidator : AbstractValidator<CreateLinkCommand>
{
    public CreateLinkCommandValidator()
    {
        RuleFor(v => v.Url)
            .NotNull()
            .NotEmpty()
            .Must(BeValidUrl).WithMessage("Must be a valid HTTP URL.");
    }

    public static bool BeValidUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return false;
        }

        bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                      && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                      && Uri.IsWellFormedUriString(url, UriKind.Absolute);

        return result;
    }

}
