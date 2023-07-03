using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class UserAccountViewModel: BaseModel
    {
        public string? SearchText { get; set; }
        public List<UsersViewModel> Users { get; set; }
        public List<Office> Offices { get; set; }
    }
}
