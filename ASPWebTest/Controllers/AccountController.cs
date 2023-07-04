using ASPWebTest.Models;
using ASPWebTest.Services;
using ASPWebTest.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ASPWebTest.Controllers
{
    public class AccountController : BaseController
    {
        private LoginService loginService;
        private RoleService roleService;
        private UserAccountService userAccountService;
        public AccountController(ApplicationDbContext db,
            LoginService loginService, 
            RoleService roleService,
            UserAccountService userAccountService
            ) : base(db)
        {
            this.loginService = loginService;
            this.roleService = roleService;
            this.userAccountService = userAccountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel)
        {
            var success = false;
            var msg = "";
            if (ModelState.IsValid)
            {
                var result = await loginService.Login(viewModel);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, result.AccountId.ToString())
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        principal, new AuthenticationProperties());


                    return RedirectToAction("Index","Home");
                }
                ViewBag.Message = result.errors;

            }
            return View(viewModel);
        }
        public IActionResult Register()
        {
            RegisterViewModel viewModel = new RegisterViewModel();
            viewModel.Roles = roleService.GetAllSelectList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel viewModel) 
        {
            var success = false;
            var msg = "";
            viewModel.Roles = roleService.GetAllSelectList();
            if (ModelState.IsValid) 
            { 
                if(viewModel.Password != viewModel.ConfirmPassword)
                {
                    ViewBag.Message = "Password dan Confirm Password Tidak sama!";
                    return View(viewModel);
                }
                if(viewModel.RoleIds.Length == 0)
                {
                    ViewBag.Message = "Role harus dipilih minimal 1";
                    return View(viewModel);
                }
                var isExist = loginService.CekExist(viewModel);
                if (isExist != 0)
                {
                    msg = "Account Name already exist";
                }
                else
                {
                    var result = await loginService.Register(viewModel);
                    if (result.Succeeded)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, result.AccountId.ToString()),
                            new Claim(ClaimTypes.Name, result.UserName)
                        };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            principal, new AuthenticationProperties());

                        return RedirectToAction("Menu");
                    }
                }

            }
            else
            {
                ViewBag.Message = string.Join(" | ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                ViewBag.Message = ViewBag.Message;
            }
            return View(viewModel);
        }

        public IActionResult Menu()
        {
            var viewModel = loginService.GetMenuRegister();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Menu(List<MenuRegisterViewModel> item)
        {

            var success = false;
            var msg = "";
            int userId = string.IsNullOrEmpty(GetLoggedUser())? 0: Convert.ToInt32(GetLoggedUser());

            if (ModelState.IsValid)
            {
                //var result = await userAccountService.RegisterMenu(userId, viewModel);
                //if (result.Succeeded)
                //{
                //    return RedirectToAction("Index");
                //}
                //ViewBag.Message = result.errors;

            }
            return View(item);
        }
        public async Task<IActionResult> LogOut()
        {
            //SignOutAsync is Extension method for SignOut
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //Redirect to home page
            return LocalRedirect("/");
        }
    }
}
