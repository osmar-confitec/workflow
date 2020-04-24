using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorkFlowDominioShared.Events
{
   public interface IEventStoreSave
    {

        Task Save<T>(T theEvent) where T : StoredEvent;
    }
}
