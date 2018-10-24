using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace TodoApi.Mediatr
{
    public class SomeEvent : INotification
    {
        public SomeEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class Handler1 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler1> _logger;

        public Handler1(ILogger<Handler1> logger)
        {
            _logger = logger;
        }
       
        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }

    public class Handler2 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler2> _logger;

        public Handler2(ILogger<Handler2> logger)
        {
            _logger = logger;
        }
        public void Handle(SomeEvent notification)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
        }

        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
           _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }


}
