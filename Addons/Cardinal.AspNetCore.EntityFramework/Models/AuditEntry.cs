using Cardinal.AspNetCore.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.EntityFramework.Models
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public AuditEntity ToAudit()
        {
            var audit = new AuditEntity();
            audit.TableName = TableName;
            audit.Date = DateTime.UtcNow;
            audit.KeyValues = JsonConvert.SerializeObject(KeyValues);
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }
    }
}
