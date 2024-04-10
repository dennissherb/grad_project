﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataObjects 
{
    public class Page
    {
        [Key]
        [Column("pages_id")]
        public int Id { get; set; } = 0;

        [Required]
        [Column("pages_type")]
        public string Type { get; set; } = "Article";

        [ForeignKey("Account")]
        [Column("pages_author_id")]
        public int AuthorId { get; set; } = 0;

        [ForeignKey("Product")]
        [Column("fkproducts_id")]
        public int? ProductId { get; set; } = null;

        [Column("pages_tags")]
        public string? Tags { get; set; } = null;

        [Column("pages_content")]
        public string? Content { get; set; } = "<h1>This page is empty</h1>";


        // Navigation propety
        public Account Author { get; set; }
    }
}