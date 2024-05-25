namespace LinkIo.Application.Links.Commands.EditLink;
public record EditLinkCommand : IRequest
{
    public int Id { get; init; }
}
