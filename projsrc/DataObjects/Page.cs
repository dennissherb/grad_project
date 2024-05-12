using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataObjects 
{
    public class Page
    {
        [Key]
        [Column("pages_id")]
        public int? Id { get; set; }

        [Required]
        [Column("pages_type")]
        public string? Type { get; set; }

        [ForeignKey("Account")]
        [Column("pages_author_id")]
        public int? AuthorId { get; set; }

        [ForeignKey("Product")]
        [Column("fkproducts_id")]
        public int? ProductId { get; set; }

        [Column("pages_tags")]
        public string? Tags { get; set; }

        [Column("pages_content")]
        public string? Content { get; set; }

        [Column("pages_title")]
        public string? Title { get; set; }

        [Column("pages_date")]
        public DateTime Date { get; set; } = DateTime.Now;

        // Navigation propety
        [JsonIgnore]
        public Account? Author { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}