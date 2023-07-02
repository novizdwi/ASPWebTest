using ASPWebTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db;

        public BaseController(ApplicationDbContext db) {
            this.db = db;
        }
    }
}
