using ASPWebTest.Models;
using ASPWebTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    public class OfficeController : Controller
    {
        private OfficeService officeService;
        public OfficeController(OfficeService officeService)
        {
            this.officeService = officeService;
        }


        public IActionResult Index( )
        {
            return View();
        }
    }
}
