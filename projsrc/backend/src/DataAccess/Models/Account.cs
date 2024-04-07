using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datalayer.Models
{
    [Table("accounts")]
    public class Account
    {
        [Key]
        [Column("accounts_id")]
        public int Id { get; set; } = 0;

        [Required]
        [Column("accounts_email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("accounts_user_name")]
        public string UserName { get; set; } = string.Empty;

        [Column("accounts_date_of_birth")]
        public DateTime DateOfBirth { get; set; } = DateTime.Now;

        [Required]
        [Column("accounts_password")]
        public string Password { get; set; } = string.Empty;

        [Column("accounts_perm_group")]
        public string PermGroup { get; set; } = "user";

        [Column("accounts_salt_column")]
        public string Salt { get; set; } = "0";
    }
}
