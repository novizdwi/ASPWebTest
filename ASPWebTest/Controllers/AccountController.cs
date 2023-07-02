using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
