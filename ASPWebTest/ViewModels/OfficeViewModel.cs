using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class OfficeViewModel : BaseModel
    {
        public string? SearchText { get; set; }
        public bool CanCreate { get; set; } = false;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;

        public List<Office> Offices { get; set; }
    }


}
