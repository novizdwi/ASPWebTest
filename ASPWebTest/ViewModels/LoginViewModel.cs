using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPWebTest.ViewModels
{
    public class LoginViewModel
    {
        [Required, DisplayName("Account Name")]
        public string? AccountName { get; set; }
        [Required, DataType(DataType.Password), DisplayName("Password")]
        public string? Password { get; set; }
    }

    public class UserLoginViewModel
    {
        public int? AccountId { get; set; }
        public int? UserAccountId { get; set; }
        public string? AccountName { get; set; }
    }

    public class MenuRegisterViewModel
    {
        public int? MenuId { get; set; }
        public string? MenuName { get; set; }
        public bool CanRead { get; set; }
        public bool CanCreate { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
    }


}
