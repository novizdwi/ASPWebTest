using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Reflection;

namespace ASPWebTest.Controllers
{
    [Authorize]
    public class UserAccountController : BaseController
    {
        private UserAccountService userAccountService;
        private OfficeService officeService;
        private MenuService menuService;

        public UserAccountController(ApplicationDbContext db,
            UserAccountService userAccountService,
            OfficeService officeService,
            MenuService menuService) : base(db)
        {
            this.userAccountService = userAccountService;
            this.officeService = officeService;
            this.menuService = menuService;
        }
        public IActionResult Index(string SearchString = null)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "UserAccount", "Read"))
            {
                return RedirectToAction("Index", "Home");
            }

            UserAccountViewModel viewModel = new UserAccountViewModel();
            viewModel.SearchText = SearchString;
            viewModel.CanCreate = menuService.CheckAuthorize(userId, "UserAccount", "Create");
            viewModel.CanUpdate = menuService.CheckAuthorize(userId, "UserAccount", "Update");
            viewModel.CanDelete = menuService.CheckAuthorize(userId, "UserAccount", "Delete");
            viewModel.Users = userAccountService.GetUsersViewModel(SearchString);
            return View(viewModel);
        }
        public IActionResult New()
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "UserAccount", "Create"))
            {
                return RedirectToAction("Index", "Home");
            }
            UsersViewModel viewModel = new UsersViewModel();
            viewModel.Offices = officeService.GetAll();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> New(UsersViewModel model)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await userAccountService.Add(model);
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
            if (!menuService.CheckAuthorize(userId, "UserAccount", "Update"))
            {
                return RedirectToAction("Index", "Home");
            }

            UsersViewModel viewModel = userAccountService.GetById(Id);
            if (viewModel == null)
            {
                return RedirectToAction("Index");
            }
            viewModel.Offices = officeService.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UsersViewModel model)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var result = await userAccountService.Update(model);
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
            if (!menuService.CheckAuthorize(userId, "UserAccount", "Delete"))
            {
                return RedirectToAction("Index", "Home");
            }

            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await userAccountService.Delete(Id);

            }

            ViewBag.Message = msg;
            return RedirectToAction("Index");
        }
    }
}
