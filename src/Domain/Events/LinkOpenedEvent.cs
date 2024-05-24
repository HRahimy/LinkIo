namespace LinkIo.Domain.Events;
public class LinkOpenedEvent : BaseEvent
{
    public LinkOpenedEvent(Link link, LinkReferrer referrer)
    {
        Link = link;
        Referrer = referrer;
    }

    public Link Link { get; }
    public LinkReferrer Referrer { get; }
}
