
using LinkIo.Application.Common.Models;
using LinkIo.Application.Links.Commands.CreateLink;
using LinkIo.Application.Links.Commands.DeleteLink;
using LinkIo.Application.Links.Commands.EditLink;
using LinkIo.Application.Links.Models;
using LinkIo.Application.Links.Queries.GetLinkDetailed;
using LinkIo.Application.Links.Queries.GetLinks;

namespace LinkIo.Web.Endpoints;

public class Links : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetLinks)
            .MapGet(GetLinkDetails, "{id}")
            .MapPost(CreateLink)
            .MapPut(EditLink, "{id}")
            .MapDelete(DeleteLink, "{id}");
    }

    public async Task<PaginatedList<LinkDto>> GetLinks(ISender sender, [AsParameters] GetLinksQuery query)
    {
        return await sender.Send(query);
    }

    public async Task<LinkDetailsDto> GetLinkDetails(ISender sender, int id)
    {
        return await sender.Send(new GetLinkDetailedQuery { Id = id });
    }

    public Task<LinkDto> CreateLink(ISender sender, CreateLinkCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> EditLink(ISender sender, int id, EditLinkCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteLink(ISender sender, int id)
    {
        await sender.Send(new DeleteLinkCommand { Id = id });
        return Results.NoContent();
    }
}
