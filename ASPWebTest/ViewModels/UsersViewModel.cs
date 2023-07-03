using ASPWebTest.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ASPWebTest.ViewModels
{
    public class UsersViewModel: BaseModel
    {
        public int? Id { get; set; }
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
        public string? OfficeName { get; set; }
    }
}
