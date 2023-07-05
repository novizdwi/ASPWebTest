using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPWebTest.Controllers
{
    [Authorize]
    public class OfficeController : BaseController
    {
        private OfficeService officeService;
        private MenuService menuService;
        public OfficeController(ApplicationDbContext db,
            OfficeService officeService,
            MenuService menuService) : base(db)
        {
            this.officeService = officeService;
            this.menuService = menuService;
        }


        public IActionResult Index(string SearchString = null)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if(!menuService.CheckAuthorize(userId, "Office", "Read"))
            {
                return RedirectToAction("Index","Home");
            }

            OfficeViewModel viewModel = new OfficeViewModel();
            viewModel.SearchText = SearchString;
            viewModel.CanCreate = menuService.CheckAuthorize(userId, "Office", "Create");
            viewModel.CanUpdate = menuService.CheckAuthorize(userId, "Office", "Update");
            viewModel.CanDelete = menuService.CheckAuthorize(userId, "Office", "Delete");
            viewModel.Offices = officeService.GetAll(SearchString);
            return View(viewModel);
        }
        public IActionResult New() 
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Office", "Create"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(Office model) 
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid) 
            {
                var isExist = officeService.CekExist(model);
                if (isExist != 0)
                {
                    msg = "Office Code already exist";
                }
                else
                {
                    var result = await officeService.Add(model);
                    if (result.Succeeded) 
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            ViewBag.Message = msg;
            return View(model);
        }
        public IActionResult Edit(int Id)
        {
            int userId = GetLoggedUser() == null ? 0 : Convert.ToInt32(GetLoggedUser());
            if (!menuService.CheckAuthorize(userId, "Office", "Update"))
            {
                return RedirectToAction("Index", "Home");
            }

            Office model = officeService.GetById(Id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Office model)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    var result = await officeService.Update(model);
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
            if (!menuService.CheckAuthorize(userId, "Office", "Delete"))
            {
                return RedirectToAction("Index", "Home");
            }

            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await officeService.Delete(Id);

            }

            ViewBag.Message = msg;
            return RedirectToAction("Index");
        }

    }
}
