namespace LinkIo.Domain.Events;
public class LinkDeletedEvent : BaseEvent
{
    public LinkDeletedEvent(Link link)
    {
        Link = link;
    }

    public Link Link { get; }
}
