using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cardinal.AspNetCore.Entities
{
    [Table("Audit")]
    public class AuditEntity
    {
        public Guid Id { get; set; }

        public string TableName { get; set; }

        public DateTime Date { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

    }
}
