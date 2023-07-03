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
        public OfficeController(ApplicationDbContext db,
            OfficeService officeService) : base(db)
        {
            this.officeService = officeService;
        }


        public IActionResult Index(string SearchString = null)
        {
            
            OfficeViewModel viewModel = new OfficeViewModel();
            viewModel.SearchText = SearchString;
            viewModel.Offices = officeService.GetAll(SearchString);
            return View(viewModel);
        }
        public IActionResult New() 
        { 
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
            Office model = officeService.GetById(Id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            return View();
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
