using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataObjects
{
    public class Product
    {
        [Key]
        [Column("products_id")]
        public int Id { get; set; }

        [Required]
        [Column("products_name")]
        public string Name { get; set; }

        [Column("products_price")]
        public decimal Price { get; set; }

        [Required]
        [Column("products_company")]
        public string Company { get; set; }

        [Column("products_category")]
        public string Category { get; set; } = "Other";

        [Column("products_image")]
        public byte[]? Image { get; set; } = null;

        [JsonIgnore]
        public ICollection<Page>? Pages { get; set; }
    }
}