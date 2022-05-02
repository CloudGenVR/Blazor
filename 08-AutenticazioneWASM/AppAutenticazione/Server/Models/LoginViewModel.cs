using System.ComponentModel.DataAnnotations;

namespace AppAutenticazione.Server.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginUserResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public string JwtToken { get; set; } = string.Empty;
        public bool UserNotExist { get; set; }
    }
}
