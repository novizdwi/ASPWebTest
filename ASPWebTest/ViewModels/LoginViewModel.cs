using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPWebTest.ViewModels
{
    public class LoginViewModel
    {
        [Required, DisplayName("Account Name")]
        public string? AccountName { get; set; }
        [Required, DisplayName("Password")]
        public string? Password { get; set; }
    }

    public class UserLoginViewModel
    {
        public int? AccountId { get; set; }
        public int? UserAccountId { get; set; }
        public string? AccountName { get; set; }
    }
}
