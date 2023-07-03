using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASPWebTest.Controllers
{
    public class UserAccountController : BaseController
    {
        private UserAccountService userAccountService;
        private OfficeService officeService;
        public UserAccountController(ApplicationDbContext db,
            UserAccountService userAccountService,
            OfficeService officeService) : base(db)
        {
            this.userAccountService = userAccountService;
            this.officeService = officeService;
        }
        public IActionResult Index(string SearchString = null)
        {
            UserAccountViewModel viewModel = new UserAccountViewModel();
            viewModel.SearchText = SearchString;
            viewModel.Users = userAccountService.GetUsersViewModel(SearchString);
            viewModel.Offices = officeService.GetAll();
            return View(viewModel);
        }
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(UserAccount model)
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
            UserAccount model = userAccountService.GetById(Id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserAccount model)
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
