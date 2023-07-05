using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private RoleService roleService;
        private MenuService menuService;
        public RoleController(ApplicationDbContext db,
            RoleService roleService,
            MenuService menuService) : base(db)
        {
            this.roleService = roleService;
            this.menuService = menuService;
        }
        public IActionResult Index(string SearchString = null)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Role", "Read"))
            {
                return RedirectToAction("Index", "Home");
            }

            RoleViewModel viewModel = new RoleViewModel();
            viewModel.SearchText = SearchString;
            viewModel.CanCreate = menuService.CheckAuthorize(userId, "Role", "Create");
            viewModel.CanUpdate = menuService.CheckAuthorize(userId, "Role", "Update");
            viewModel.CanDelete = menuService.CheckAuthorize(userId, "Role", "Delete");
            viewModel.Roles = roleService.GetAll(SearchString);
            return View(viewModel);
        }

        public IActionResult New()
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Role", "Create"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(Role model)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await roleService.Add(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Message = msg;
            return View(model);
        }

        public IActionResult Edit(int Id)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Role", "Update"))
            {
                return RedirectToAction("Index", "Home");
            }

            Role model = roleService.GetById(Id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Role model)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var result = await roleService.Update(model);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }

            ViewBag.Message = msg;
            return View(model);
        }

        public async Task<ActionResult> Delete(int Id)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Role", "Delete"))
            {
                return RedirectToAction("Index", "Home");
            }

            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await roleService.Delete(Id);

            }

            ViewBag.Message = msg;
            return RedirectToAction("Index");
        }
    }
}
