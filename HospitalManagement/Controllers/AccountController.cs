using HospitalManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HospitalManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "Appointment");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Appointment");

                    //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    //var confirmationLink = Url.Action("ConfirmEmail", "Account",
                    //                        new { userId = user.Id, token = token }, Request.Scheme);

                    //logger.Log(LogLevel.Warning, confirmationLink);

                   

                    //ViewBag.ErrorTitle = "Registration successful";
                    //ViewBag.ErrorMessage = "Before you can Login, please confirm your " +
                    //    "email, by clicking on the confirmation link we have emailed you";
                    //return View("Error");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

       

        [HttpGet]
        [AllowAnonymous]
        public  IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel
            {
                //ReturnUrl = returnUrl,
                //ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
           

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                        model.RememberMe, false);

                if (result.Succeeded)
                {

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "Appointment");
                      

                    }

                    if (signInManager.IsSignedIn(User) && User.IsInRole("Assistant"))
                    {

                        string? email = User.Identity?.Name; // Retrieve email or any identifier

                        return RedirectToActionPermanent("ViewAssistantSch", "AssistantSchedules", new { email });
                    }
                    if (signInManager.IsSignedIn(User) && User.IsInRole("Instructor"))
                    {
                        string? email = User.Identity?.Name; // Retrieve email or any identifier

                        return RedirectToActionPermanent("ViewInstructorSch", "InstructorSchedules", new { email });

                    }
                    else if (signInManager.IsSignedIn(User))
                    {
                        return RedirectToAction("Index", "Appointment");
                    }
                }

                if(result.IsLockedOut)
                {
                    return View("AccountLocked");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }


       
    }
}