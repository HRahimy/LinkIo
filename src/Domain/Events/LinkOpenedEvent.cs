namespace LinkIo.Domain.Events;
public class LinkOpenedEvent : BaseEvent
{
    public LinkOpenedEvent(LinkReferrer referrer)
    {
        Referrer = referrer;
    }

    public LinkReferrer Referrer { get; }
}
