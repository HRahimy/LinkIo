
using LinkIo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace LinkIo.Application.Links.EventHandlers;
public class LinkCreatedEventHandler : INotificationHandler<LinkCreatedEvent>
{
    private readonly ILogger<LinkCreatedEventHandler> _logger;
    public LinkCreatedEventHandler(ILogger<LinkCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LinkCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LinkIo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
