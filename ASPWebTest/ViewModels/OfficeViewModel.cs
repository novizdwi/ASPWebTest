using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class OfficeViewModel : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required, DisplayName("Office Code")]
        public string? OfficeCode { get; set; }
        [Required, DisplayName("Office Name")] 
        public string? OfficeName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? CurrentFilter { get; set; }
    }
}
