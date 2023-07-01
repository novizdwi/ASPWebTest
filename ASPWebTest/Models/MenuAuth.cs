using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.Models
{
    [Table("MenuAuth")]
    public class MenuAuth: BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? MenuId { get; set; }
        public bool? CanRead { get; set; }
        public bool? CanCreate { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanDelete { get; set; }

    }
}
