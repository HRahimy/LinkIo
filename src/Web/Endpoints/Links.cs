
using System;
using LinkIo.Application.Common.Models;
using LinkIo.Application.Links.Commands.CreateLink;
using LinkIo.Application.Links.Models;
using LinkIo.Application.Links.Queries.GetLinkDetailed;
using LinkIo.Application.Links.Queries.GetLinks;
using LinkIo.Application.Links.Queries.GetRedirectUrl;
using Microsoft.AspNetCore.Mvc;

namespace LinkIo.Web.Endpoints;

public class Links : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetLinks)
            .MapGet(GetLinkDetails, "{id}")
            .MapGet(RedirectShortUrl, "red/{shortcode}")
            .MapPost(CreateLink);
    }

    public async Task<PaginatedList<LinkDto>> GetLinks(ISender sender, [AsParameters] GetLinksQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<LinkDetailsDto> GetLinkDetails(ISender sender, int id)
    {
        return await sender.Send(new GetLinkDetailedQuery { Id = id });
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
