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
