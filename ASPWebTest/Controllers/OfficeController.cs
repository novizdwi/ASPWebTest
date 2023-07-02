using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
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


        public IActionResult Index(string searchText = null)
        {
            var viewModel = officeService.GetAll(searchText);
            return View(viewModel);
        }
    }
}
