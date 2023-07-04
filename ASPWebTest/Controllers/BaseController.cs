using ASPWebTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace ASPWebTest.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db;

        public BaseController(ApplicationDbContext db) {
            this.db = db;
        }
        protected string GetLoggedUser() {
            var query = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name);
            string ret = "";
            if (query != null)
                ret = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value;
            return ret;
        }

        protected List<object> GetModelStateError()
        {
            var data = (from a in ModelState
                        from b in a.Value.Errors
                        select new
                        {
                            Field = a.Key,
                            Message = b.ErrorMessage
                        });
            return data.ToList<object>();
        }

    }
}
