namespace LinkIo.Domain.Events;
public class LinkEditedEvent : BaseEvent
{
    public LinkEditedEvent(Link link)
    {
        Link = link;
    }

    public Link Link { get; }
}
