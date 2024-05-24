namespace LinkIo.Domain.Events;
public class LinkCreatedEvent : BaseEvent
{
    public LinkCreatedEvent(Link link)
    {
        Link = link;
    }

    public Link Link { get; }
}
