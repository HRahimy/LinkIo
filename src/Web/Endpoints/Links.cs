
using System;
using LinkIo.Application.Links.Commands.CreateLink;
using LinkIo.Application.Links.Models;
using LinkIo.Application.Links.Queries.GetRedirectUrl;
using Microsoft.AspNetCore.Mvc;

namespace LinkIo.Web.Endpoints;

public class Links : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(RedirectShortUrl, "red/{shortcode}")
            .MapPost(CreateLink);
    }

    public async Task<IResult> RedirectShortUrl(ISender sender, string shortcode, HttpRequest request)
    {
        var result = await sender.Send(new GetRedirectUrlQuery { ShortUrlCode = shortcode, ReferrerUrl = request.Headers.Referer });

        return Results.Redirect(result);
    }

    public Task<LinkDto> CreateLink(ISender sender, CreateLinkCommand command)
    {
        return sender.Send(command);
    }
}
