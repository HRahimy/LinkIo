﻿using LinkIo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace LinkIo.Application.Links.EventHandlers;
public class LinkOpenedEventHandler : INotificationHandler<LinkOpenedEvent>
{
    private readonly ILogger<LinkOpenedEventHandler> _logger;
    public LinkOpenedEventHandler(ILogger<LinkOpenedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LinkOpenedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("LinkIo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
