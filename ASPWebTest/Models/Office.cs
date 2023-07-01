using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.Models
{
    [Table("Office")]
    public class Office: BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? OfficeCode { get; set; }
        public string? OfficeName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }

    }
}
