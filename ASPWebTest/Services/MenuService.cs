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

        public bool GetMenu(int userId, string MenuName, string Method) {
            MenuAuth menu = (
                from T0 in db.MenuAuths
                join T1 in db.Menus on T0.MenuId equals T1.Id
                where T0.UserId == userId && T1.MenuName == MenuName
                select T0).FirstOrDefault();
            var ret = false;

            if (Method == "Read") ret = menu.CanRead??false;
            else if (Method == "Create") ret = menu.CanCreate??false;
            else if (Method == "Edit") ret = menu.CanEdit??false;
            else if (Method == "Delete") ret = menu.CanDelete??false;
            
            return ret;
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
