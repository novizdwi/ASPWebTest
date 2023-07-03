using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPWebTest.Models
{
    [Table("AccountRole")]
    public class AccountRole : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public int? AccountId { get; set; }
        public int? RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
