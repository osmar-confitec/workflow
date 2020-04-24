using System;
using System.Collections.Generic;
using System.Text;

namespace WorkFlowDominioShared.Events
{

    public enum StoredEventActionEnum
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }
    public abstract class StoredEvent : Entity
    {


        public StoredEvent(string data, Guid user, StoredEventActionEnum _storedEventAction) : base()
        {
            Data = data;
            UserId = user;
            Timestamp = DateTime.Now;
            StoredEventAction = _storedEventAction;
        }
        // EF Constructor
        protected StoredEvent() : base() { MessageType = GetType().Name; Timestamp = DateTime.Now; }

        public StoredEventActionEnum StoredEventAction { get; set; }

        public string Data { get; set; }

        public Guid UserId { get; set; }

        public Guid AggregatedId { get; set; }

        public DateTime Timestamp { get; set; }

        public string MessageType { get; set; }


    }
}
