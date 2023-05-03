using System.ComponentModel.DataAnnotations;

namespace FinanceAssemblyApp.Models
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "User name should not be empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password should not be empty")]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
