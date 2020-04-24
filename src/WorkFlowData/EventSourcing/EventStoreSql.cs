using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorkFlowData.EventSourcing.Context;
using WorkFlowDominio.Entities.StoreEvents;
using WorkFlowDominioShared.Events;

namespace WorkFlowData.EventSourcing
{
    public class EventStoreSql : IEventStoreSave
    {

        readonly EventStoreContexto _eventStoreContexto;
        
        public EventStoreSql(EventStoreContexto eventStoreContexto)
        {
            _eventStoreContexto = eventStoreContexto;

        }

        public  Task Save<T>(T theEvent) where T : StoredEvent
        {
            _eventStoreContexto.Add(theEvent);
            _eventStoreContexto.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
