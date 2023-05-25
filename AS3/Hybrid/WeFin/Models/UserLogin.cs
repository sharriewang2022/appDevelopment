using System.ComponentModel.DataAnnotations;

namespace WeFin.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(60, ErrorMessage = "User name length can't be more than 60.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "User password length can't be more than 50.")]
        public string UserPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "AccountNo length can't be more than 100.")]
        public string AccountNo { get; set; }

        [StringLength(100)]
        public string AccessToken { get; set; }

        [StringLength(100)]
        public string RefreshToken { get; set; }

        [StringLength(100, ErrorMessage = "UserAvatar length can't be more than 100.")]
        public string UserAvatar { get; set; }

        public bool LoginFailureHidden { get; set; } = true;
    }
}
