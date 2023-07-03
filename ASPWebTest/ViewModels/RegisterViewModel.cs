using ASPWebTest.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.ViewModels
{
    public class RegisterViewModel
    {
        [Required, DisplayName("Account Name")]
        public string? AccountName { get; set; }
        [Required, DataType(DataType.Password), DisplayName("Password")]
        public string? Password { get; set; }
        [Required, DataType(DataType.Password), DisplayName("Confirm Password")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public int[]? RoleIds { get; set; }
        public List<SelectListItem>? Roles { get; set; }

    }
}
