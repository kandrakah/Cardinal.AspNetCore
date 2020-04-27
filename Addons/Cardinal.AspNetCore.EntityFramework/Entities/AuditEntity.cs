using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cardinal.AspNetCore.Entities
{
    [Table("Audit")]
    public class AuditEntity : Entity
    {
        public string TableName { get; set; }

        public DateTime Date { get; set; }

        public string KeyValues { get; set; }

        public string OldValues { get; set; }

        public string NewValues { get; set; }

    }
}
