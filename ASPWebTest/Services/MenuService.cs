using ASPWebTest.Models;
using ASPWebTest.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASPWebTest.Services
{

    public class MenuService : BaseService
    {
        private ApplicationDbContext db;
        public MenuService(ApplicationDbContext db) {
            this.db = db;
        }

        public async Task<List<MenuViewModel>> GetMenu()
        {
            var ret = (from T0 in db.Menus
                       select new MenuViewModel()
                       {
                           MenuName = T0.MenuName,
                           Description = T0.Description,
                       });
            return await ret.ToListAsync();
        }
    }
}
