using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.ViewModels
{
    public class RegisterViewModel
    {
        [Required, DisplayName("Account Name")]
        public string? AccountName { get; set; }
        [Required, DisplayName("Password")]
        public string? Password { get; set; }
        [Required, DisplayName("Confirm Password")]
        public string? ConfirmPassword { get; set; }
    }
}
