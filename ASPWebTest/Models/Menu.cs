using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASPWebTest.Models
{
    [Table("Menu")]
    public class Menu : BaseModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public string? MenuName { get; set; }
        public string? Description { get; set; }

    }
}
