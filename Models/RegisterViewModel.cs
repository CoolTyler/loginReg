using System.ComponentModel.DataAnnotations;
namespace loginRegistration.Models
{
        public class RegisterViewModel
        {
                [Required]
                [Display(Name = "First Name")]
                [MinLength(2, ErrorMessage="First name must be at least 2 characters!")]
                public string firstName { get; set; }

                [Required]
                [Display(Name = "Last Name")]
                [MinLength(2)]
                public string lastName { get; set; }

                [Required]
                [Display(Name = "Email Address")]
                [EmailAddress]
                public string email { get; set; }

                [Required]
                [Display(Name = "Password")]
                [MinLength(8)]
                [DataType(DataType.Password)]
                public string password { get; set; }

                [Compare("password", ErrorMessage = "Password and confirmation do not match.")]
                [DataType(DataType.Password)]
                public string confirmPassword { get; set; }
        }
}