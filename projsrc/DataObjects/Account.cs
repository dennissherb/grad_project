﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace DataObjects
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [Column("accounts_id")]
        public int Id { get; set; }

        [Required]
        [Column("accounts_email")]
        public string? Email { get; set; }

        [Required]
        [Column("accounts_user_name")]
        public string? UserName { get; set; }

        [Column("accounts_date_of_birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required]
        [Column("accounts_password")]
        public string? Password { get; set; }

        [Column("accounts_perm_group")]
        public string? PermGroup { get; set; } = "user";

        [Column("accounts_salt_column")]
        public string? Salt { get; set; }

        [JsonIgnore]
        public ICollection<Page>? Pages { get; set; }

        [JsonIgnore]
        public ICollection<Reply>? Replies { get; set; }
    }
}
