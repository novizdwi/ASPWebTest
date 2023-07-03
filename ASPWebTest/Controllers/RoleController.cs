using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    public class RoleController : BaseController
    {
        private RoleService roleService;
        public RoleController(ApplicationDbContext db,
            RoleService roleService) : base(db)
        {
            this.roleService = roleService;
        }
        public IActionResult Index(string SearchString = null)
        {
            RoleViewModel viewModel = new RoleViewModel();
            viewModel.SearchText = SearchString;
            viewModel.Roles = roleService.GetAll(SearchString);
            return View(viewModel);
        }

        public IActionResult New()
        {
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
