using ASPWebTest.Models;
using ASPWebTest.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Transactions;

namespace ASPWebTest.Services
{

    public class MenuService : BaseService
    {
        private ApplicationDbContext db;
        public MenuService(ApplicationDbContext db) {
            this.db = db;
        }

        public bool CheckAuthorize(int userId, string menuName, string accessType) { 
            bool ret = false;
            if(userId !=0 && !string.IsNullOrEmpty(menuName) && !string.IsNullOrEmpty(accessType))
            {
                var query = (from T0 in db.MenuAuths.Where(x=> x.UserId == userId) 
                             join T1 in db.Menus on T0.MenuId equals T1.Id
                             where T1.MenuName == menuName
                             select new MenuRegisterViewModel() {
                                MenuId = T0.MenuId,
                                MenuName = T1.Description,
                                CanRead = T0.CanRead??false,
                                CanCreate = T0.CanCreate??false,
                                CanUpdate = T0.CanEdit??false,
                                CanDelete = T0.CanDelete??false,
                             }).FirstOrDefault();
                if (query != null)
                {
                    if (accessType == "Create") ret = query.CanCreate;
                    if (accessType == "Read") ret = query.CanRead;
                    if(accessType == "Update") ret = query.CanUpdate;
                    if (accessType == "Delete") ret = query.CanDelete;
                }
            }

            return ret;
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

        public List<MenuRegisterViewModel> GetMenuRegister(int userId = 0)
        {

            if (userId == 0) {
                return (from T0 in db.Menus
                        select new MenuRegisterViewModel()
                        {
                            MenuId = T0.Id,
                            MenuName = T0.Description,
                            CanCreate = false,
                            CanRead = false,
                            CanUpdate = false,
                            CanDelete = false,
                        }).ToList();
            }
            else {
                var countMenus = db.MenuAuths.Where(x => x.UserId == userId).Count();
                if(countMenus == 0)
                {
                    return (from T0 in db.Menus
                            select new MenuRegisterViewModel()
                            {
                                MenuId = T0.Id,
                                MenuName = T0.Description,
                                CanCreate = false,
                                CanRead = false,
                                CanUpdate = false,
                                CanDelete = false,
                            }).ToList();
                }

                return (from T0 in db.MenuAuths.Where(x=> x.UserId == userId)
                        join T1 in db.Menus on T0.MenuId equals T1.Id
                    select new MenuRegisterViewModel()
                    {
                        MenuId = T0.Id,
                        MenuName = T1.Description,
                        CanCreate = T0.CanCreate??false,
                        CanRead = T0.CanRead ?? false,
                        CanUpdate = T0.CanEdit ?? false,
                        CanDelete = T0.CanDelete ?? false,
                    }).ToList();

            }
        }

        public async Task<OperationResult> RegisterMenu(int userId, List<MenuRegisterViewModel> viewModel)
        {
            if(userId == 0)
            {
                return OperationResult.Failed("No User Login ");
            }

            try
            {
                using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                TimeSpan.FromMinutes(60),
                TransactionScopeAsyncFlowOption.Enabled
                )) 
                {
                    foreach(var vm in viewModel)
                    {
                        var menuId = db.MenuAuths.Where(x => x.UserId == userId && x.MenuId == vm.MenuId).Select(x=> x.Id).FirstOrDefault();
                        if(menuId == null) {
                            AddMenu(userId, vm);
                        } else {
                            UpdateMenu(userId, vm);
                        }

                    }
                    scope.Complete();
                    return OperationResult.Success();

                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.ToString());
            }
        }
        public void AddMenu(int userId, MenuRegisterViewModel viewModel) {
            try
            {
                var data = new MenuAuth()
                {
                    UserId = userId,
                    MenuId = viewModel.MenuId,
                    CanCreate = viewModel.CanCreate,
                    CanRead = viewModel.CanRead,
                    CanDelete = viewModel.CanDelete,
                    CanEdit = viewModel.CanUpdate,

                    CreatedDate = DateTime.Now,
                    CreatedUser = userId,
                };
                db.MenuAuths.Add(data);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
        public void UpdateMenu(int userId, MenuRegisterViewModel vm) {
            try {
                var data = db.MenuAuths.Find(vm.MenuId);
                if (data != null)
                {
                    data.CanCreate = vm.CanCreate;
                    data.CanRead = vm.CanRead;
                    data.CanDelete = vm.CanDelete;
                    data.CanEdit = vm.CanUpdate;

                    data.ModifiedDate = DateTime.Now;
                    data.ModifiedUser = userId;
                    db.SaveChanges();

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


    }
}
