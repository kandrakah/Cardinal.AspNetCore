using IdentityServer4.Events;
using System;

namespace Cardinal.AspNetCore.EventStore.Events
{
    public class EventEntity
    {
        public Guid Id { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public EventTypes EventType { get; set; }

        public string Message { get; set; }

        public string ActivityId { get; set; }

        public int ProcessId { get; set; }

        public string LocalIpAddress { get; set; }

        public string RemoteIpAddress { get; set; }

        public string AggregateId { get; set; }

        public DateTime Timestamp { get; set; }

        public string User { get; private set; }

        public EventDetails Details { get; set; }

        public int EventId { get; set; }

        public EventEntity SetAggregate(string aggregateId)
        {
            AggregateId = aggregateId;
            return this;
        }

        public EventEntity SetUser(string user)
        {
            this.User = user;
            return this;
        }
    }
}
