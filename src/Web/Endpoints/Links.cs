
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
            .MapGet(GetLinkDetails, "{id}");
    }

    public async Task<PaginatedList<LinkDto>> GetLinks(ISender sender, [AsParameters] GetLinksQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<LinkDetailsDto> GetLinkDetails(ISender sender, int id)
    {
        return await sender.Send(new GetLinkDetailedQuery { Id = id });
    }
}
