using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebTest.Models
{
    [Table("Account")]
    public class Account : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? AccountName { get; set; }
        public string? Password { get; set; }

    }
}
