using System.ComponentModel.DataAnnotations;
namespace loginRegistration.Models
{
        public class LoginViewModel
        {
                [Required(ErrorMessage = "Email address cannot be left blank")]
		public string email { get; set; }
		[Required(ErrorMessage = "Password cannot be left blank")]
		public string password { get; set; }
        }
}