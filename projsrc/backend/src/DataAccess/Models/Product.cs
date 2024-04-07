using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models
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

        [Required]
        [Column("products_category")]
        public string Category { get; set; }

        [Column("products_image")]
        public byte[]? Image { get; set; } = null;
    }
}