
using LinkIo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace LinkIo.Application.Links.EventHandlers;
public class LinkEditedEventHandler : INotificationHandler<LinkEditedEvent>
{
    private readonly ILogger<LinkEditedEventHandler> _logger;
    public LinkEditedEventHandler(ILogger<LinkEditedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LinkEditedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LinkIo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
