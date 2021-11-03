using CimaLek.Data;
using CimaLek.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CimaLek.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public HomeController(ILogger<HomeController> logger, UserManager<User> userMngr, SignInManager<User> signInMngr, ApplicationDbContext con, RoleManager<IdentityRole> role)
        {
            userManager = userMngr;
            signInManager = signInMngr;
            _context = con;
            roleManager = role;
            _logger = logger;
        }

        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Login(string Return = "")
        {


            var model = new AccountLogin { ReturnUrl = Return };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLogin login)
        {
            var user = await userManager.FindByNameAsync(login.Username);

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent: login.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    
                    var result1 = await userManager.UpdateAsync(user);
                    if (!string.IsNullOrEmpty(login.ReturnUrl) && Url.IsLocalUrl(login.ReturnUrl))
                    {
                        return Redirect(login.ReturnUrl);
                    }
                    else
                    {
                        if (await userManager.IsInRoleAsync(user, "Admin"))
                        {
                            return RedirectToAction("Profile", "Admin");
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }

            }
            ModelState.AddModelError("", "Invalid username/password.");
            return View(login);

        }
    }
}
