using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class UserAccountViewModel: BaseModel
    {
        public string? SearchText { get; set; }
        public bool CanCreate { get; set; } = false;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;

        public List<UsersViewModel> Users { get; set; }
    }
}
