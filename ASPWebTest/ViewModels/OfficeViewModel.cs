using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ASPWebTest.Models;

namespace ASPWebTest.ViewModels
{
    public class OfficeViewModel : BaseModel
    {
        public string? SearchText { get; set; }
        public List<Office> Offices { get; set; }
    }


}
