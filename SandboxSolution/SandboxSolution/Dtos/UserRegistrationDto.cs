using System.ComponentModel.DataAnnotations;
using System.Security;

namespace SandboxSolution.Dtos
{
    public class UserRegistrationDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(8, ErrorMessage = "Username has to at least 8 characters long")]
        [MaxLength(16, ErrorMessage = "Username can't be longer than 16 characters long")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string RepeatPassword { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Must enter valid email address")]
        [RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+\=?^_`{|}~-]+@[a-zA-Z0-9-]+\.(?:[a-zA-Z0-9-]+)*$",
            ErrorMessage = "Enter valid email address")]
        public string Email { get; set; }
    }
}
