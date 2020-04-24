using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowDominioShared.Bus;
using WorkFlowDominioShared.Events;
using WorkFlowDominioShared.Notifications;

namespace WorkFlowBus
{
    public sealed class InMemoryBusWorkFlow : IMediatorHandler
    {
        private readonly IMediator _mediator;
        readonly IEventStoreSave _eventStore;

        public InMemoryBusWorkFlow(IEventStoreSave eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        //public Task SendCommand<T>(T command) where T : Command
        //{
        //    return _mediator.Send(command);
        //}

        public  Task RaiseEvent<T>(T @event) where T : Event
        {
            if (@event.MessageType.Equals("EventStoreNotifications"))
            {
                 _eventStore?.Save((@event as EventStoreNotifications)._storedEvent);
            }
            return _mediator.Publish(@event);
        }


    }
}
