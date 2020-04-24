using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorkFlowDominio.Events;

namespace WorkFlowDominio.EventHandlers
{
    public class ClienteEventHandler :
        INotificationHandler<ClienteInseridoEvent>,
        INotificationHandler<ClienteAlteradoEvent>,
        INotificationHandler<ClienteRemovidoEvent>
    {
        public  Task Handle(ClienteRemovidoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public  Task Handle(ClienteAlteradoEvent notification, CancellationToken cancellationToken)
        {
            //
            return Task.CompletedTask;
        }

        public  Task Handle(ClienteInseridoEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
