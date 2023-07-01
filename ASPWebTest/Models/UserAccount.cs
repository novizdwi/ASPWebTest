using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.Models
{
    [Table("UserAccount")]
    public class UserAccount:BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public int? AccountId { get; set; }
        public string? FirstName { get; set; }
        public string? MidleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public int? OfficeId { get; set; }

    }
}
