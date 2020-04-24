using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkFlowDominioShared.Notifications
{
    public class EventStoreNotificationsHandler : INotificationHandler<EventStoreNotifications>
    {
        public Task Handle(EventStoreNotifications notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
