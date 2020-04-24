using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominioShared.Events;

namespace WorkFlowDominioShared.Notifications
{
    public class EventStoreNotifications : Event
    {
        public StoredEvent  _storedEvent { get; private set; }

        public EventStoreNotifications(StoredEvent storedEvent)
        {
            _storedEvent = storedEvent;
        }
    }
}
