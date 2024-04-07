using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models
{
    public class Page
    {
        [Key]
        [Column("pages_id")]
        public int Id { get; set; }

        [Required]
        [Column("pages_type")]
        public string Type { get; set; } 

        [ForeignKey("accounts")]
        [Column("pages_author_id")]
        public int AuthorId { get; set; }

        [ForeignKey("products")]
        [Column("fkproducts_id")]
        public int? ProductId { get; set; }

        [Column("pages_tags")]
        public string? Tags { get; set; } = null;

        [Column("pages_content")]
        public string? Content { get; set; } = null;

        // Navigation property
        public Product? Product { get; set; }
        public Account Author;
    }
}