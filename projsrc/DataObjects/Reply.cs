using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataObjects
{
    public class Reply
    {
        [Key]
        [Column("replies_id")]
        public int? Id { get; set; }

        [ForeignKey("Account")]
        [Column("fkaccounts_id")]
        public int? AuthorId { get; set; }

        [ForeignKey("Page")]
        [Column("fkpage_id")]
        public int? PageId { get; set; }

        [Column("replies_content")]
        public string? Content { get; set; }

        [Column("replies_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation propety
        public Account? Author { get; set; }

        [JsonIgnore]
        public Page? Page { get; set; }
    }
}
