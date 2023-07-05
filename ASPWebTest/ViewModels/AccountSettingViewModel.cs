using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.ViewModels
{
    public class AccountSettingViewModel
    {
        public string? SearchText { get; set; }
        public int? UserId { get; set; }

        [DisplayName("Change Password")]
        public bool? IsChangePassword { get; set; }
        [DataType(DataType.Password), DisplayName("Password")]
        public string? Password { get; set; }
        [DataType(DataType.Password), DisplayName("Confirm Password")]
        public string? ConfirmPassword { get; set; }
        public int? AccountId { get; set; }
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string? MidleName { get; set; }
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [DisplayName("Email")]
        public string? Email { get; set; }
        [DisplayName("Address")]
        public string? Address1 { get; set; }
        [DisplayName("Address")]
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? OfficeId { get; set; }
        public List<SelectListItem>? Offices { get; set; }
    }
}
