using ASPWebTest.Models;

namespace ASPWebTest.Services
{
    public class BaseService
    {
        protected ApplicationDbContext db;
        public BaseService() : base()
        {
            db = new ApplicationDbContext();
        }
    }
}
