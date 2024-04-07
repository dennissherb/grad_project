
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models
{
    [Table("Pages")]
    public class Page
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        [ForeignKey("Products")]
        public int? ProdId { get; set; }

        [ForeignKey("Accounts")]
        public int AuthorId { get; set; }
        public string? Tags { get; set; }
        public string? Content { get; set; }
    }
}
