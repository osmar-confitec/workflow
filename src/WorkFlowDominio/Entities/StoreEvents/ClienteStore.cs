using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominioShared.Events;


namespace WorkFlowDominio.Entities.StoreEvents
{
   public class ClienteStore: StoredEvent
    {
        public ClienteStore(Cliente cliente, StoredEventActionEnum _storedEventAction, Guid userId):base()
        {
            AggregatedId = cliente.Id;
            Data = JsonConvert.SerializeObject(cliente);
            StoredEventAction = _storedEventAction;
            UserId = userId;
        }

        public ClienteStore()
        {

        }
    }
}
