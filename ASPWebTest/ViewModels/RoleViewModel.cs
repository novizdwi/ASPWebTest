using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class RoleViewModel : BaseModel
    {
        public string? SearchText { get; set; }
        public List<Role> Roles { get; set; }
    }
}
