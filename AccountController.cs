using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestFinal.Models;
using TestFinal.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace TestFinal.Controllers
{
    //[Authorize(Roles="Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public AccountController(UserManager<ApplicationUser> userManager,
                    SignInManager<ApplicationUser> signInManager, IWebHostEnvironment _hostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            hostingEnvironment = _hostingEnvironment;
        }
        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use!");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniquePhotoFileName = null;
                if (model.ProfilePhoto != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfilePhoto.CopyTo(fs);
                    }
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Oras = model.Oras,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NumberPhone = model.NumberPhone,
                    ProfilePicture = uniquePhotoFileName
            };
                var result = await userManager.CreateAsync(user, model.Password);
                await userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");

                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

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
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }

                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditPersonalData(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User option with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditPersonalViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Oras = user.Oras,
                NumberPhone = user.NumberPhone
            };

            return View("EditProfile", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonalData(EditPersonalViewModel model)
        {
            var user = await userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                string uniquePhotoFileName = null;
                if (model.ProfilePhoto != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePhoto.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProfilePhoto.CopyTo(fs);
                    }
                }
                user.Id = model.Id;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.NumberPhone = model.NumberPhone;
                user.ProfilePicture = uniquePhotoFileName;

                await userManager.UpdateAsync(user);

                return RedirectToAction("Profile", "Account");

            }
            return View("Profile", model);
        }
    }
}

