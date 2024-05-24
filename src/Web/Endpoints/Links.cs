
using LinkIo.Application.Links.Commands.CreateLink;
using LinkIo.Application.Links.Models;

namespace LinkIo.Web.Endpoints;

public class Links : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateLink);
    }

    public Task<LinkDto> CreateLink(ISender sender, CreateLinkCommand command)
    {
        return sender.Send(command);
    }
}
