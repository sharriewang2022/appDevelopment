using SQLite;
using System.ComponentModel.DataAnnotations;

namespace WeiFin.Models
{
    [Table("gl_user")]
    public class UserRegister
    {
        // PrimaryKey is typically numeric 
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [StringLength(50), Unique]
        public string UserID { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "User name length can't be more than 60.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "User password length can't be more than 50.")]
        public string UserPassword { get; set; }
        // 0: add; 1: update
        public int IsAdd { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Email length can't be more than 100.")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "AccountNo length can't be more than 100.")]
        public string AccountNo { get; set; }

        [StringLength(100, ErrorMessage = "UserAvatar length can't be more than 100.")]
        public string UserAvatar { get; set; }

        public bool LoginFailureHidden { get; set; } = true;
    }
}
