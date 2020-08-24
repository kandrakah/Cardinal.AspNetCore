using System;

namespace Cardinal.AspNetCore.EventStore.Events
{
    public class EventDetails
    {
        public Guid EventId { get; set; }

        public string Metadata { get; set; }

        public EventEntity Event { get; set; }

        protected EventDetails()
        {

        }

        public EventDetails(Guid id, string metadata)
        {
            EventId = id;
            Metadata = metadata;
        }
    }
}
