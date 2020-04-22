using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FuelQuoteApp.DataLayer.Repo;
using FuelQuoteApp.EntityModels;
using FuelQuoteApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FuelQuoteApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IFuelQuoteRepository _FuelQuoteRepo;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, IFuelQuoteRepository FuelQuoteRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _FuelQuoteRepo = FuelQuoteRepo;
        }
        [HttpGet]
        public IActionResult Display()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded) {
                    int userID = _FuelQuoteRepo.GetUserID(model.Email);
                    User userinfo = new User
                    {
                        Id = userID,
                        Email = model.Email
                    };

                    HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(userinfo));

                    bool clientinfo = _FuelQuoteRepo.GetClientInfo(userID);
                    if(clientinfo)
                    {
                        return RedirectToAction("ClientDashBoard", "Client");
                    }
                    else
                    {
                        TempData["ClientProfileInfo"] = "Fill the profile details before requesting for a quote";
                        return RedirectToAction("ClientProfile", "Client");
                    }
                    
                }
              
                    ModelState.AddModelError(string.Empty, "Invalid Credentials!");               
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerInfo)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registerInfo.Email, Email = registerInfo.Email };
                var result= await userManager.CreateAsync(user, registerInfo.Password);

                if(result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    User userinfo = new User
                    {
                        UserName = registerInfo.UserName,
                        Email = registerInfo.Email
                    };
                    _FuelQuoteRepo.AddUser(userinfo);

                    TempData["RegistrationSuccessful"] = "You're registered succesfully!";
                    return RedirectToAction("Login", "Account");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public bool RegisterDataValidation(RegisterViewModel registerinfo)
        {
            bool flag = false;
            if ((registerinfo.UserName.Length <= 50) && (registerinfo.UserName!=String.Empty))
            {
                if ((Regex.IsMatch(registerinfo.Email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*")) && (registerinfo.Email != String.Empty))
                {
                    if(registerinfo.Password == registerinfo.ConfirmPassword)
                    {
                        flag= true;
                    }
                }
            }
            else
            {
                flag = false;
            }

            return flag;
        }
    }
}