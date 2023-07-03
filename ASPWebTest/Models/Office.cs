using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ASPWebTest.Models
{
    [Table("Office")]
    public class Office: BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        [Required, DisplayName("Office Code")]
        public string? OfficeCode { get; set; }
        [Required, DisplayName("Office Name")]
        public string? OfficeName { get; set; }
        [DisplayName("Address")]
        public string? Address1 { get; set; }
        [DisplayName("Address")]
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }

    }
}
