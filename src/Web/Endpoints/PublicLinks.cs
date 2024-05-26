
using LinkIo.Application.Links.Commands.CreateLink;
using LinkIo.Application.Links.Models;
using LinkIo.Application.Links.Queries.GetRedirectUrl;

namespace LinkIo.Web.Endpoints;

public class PublicLinks : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateLink);

        app.MapGroup(this, "short")
            .MapGet(RedirectShortUrl, "{shortcode}");
    }

    public async Task<IResult> RedirectShortUrl(ISender sender, string shortcode, HttpRequest request)
    {
        var result = await sender.Send(new GetRedirectUrlQuery { ShortUrlCode = shortcode, ReferrerUrl = request.Headers.Referer });

        return Results.Redirect(result);
    }

    public Task<LinkDto> CreateLink(ISender sender, CreatePublicLinkCommand command)
    {
        return sender.Send(command);
    }
}
