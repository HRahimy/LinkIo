using LinkIo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace LinkIo.Application.Links.EventHandlers;
public class LinkDeletedEventHandler : INotificationHandler<LinkDeletedEvent>
{
    private readonly ILogger<LinkDeletedEventHandler> _logger;

    public LinkDeletedEventHandler(ILogger<LinkDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LinkDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LinkIo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
