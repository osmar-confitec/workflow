using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowDominioShared.Events;

namespace WorkFlowDominio.Entities.StoreEvents
{
   public class UsuarioStore : StoredEvent
    {
        public UsuarioStore(Usuario usuario, StoredEventActionEnum _storedEventAction, Guid userId) : base()
        {
            AggregatedId = usuario.Id;
            Data = JsonConvert.SerializeObject(usuario);
            StoredEventAction = _storedEventAction;
            UserId = userId;
        }

        public UsuarioStore()
        {

        }
    }
}
