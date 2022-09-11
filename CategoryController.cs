using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;
using TestFinal.Models.Repositores;
using TestFinal.ViewModels;

namespace TestFinal.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly ICategoryServices categoryService;
        private readonly IProductService productService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserManager<ApplicationUser> UserManager { get; }

        public CategoryController(IWebHostEnvironment _hostingEnvironment, ICategoryServices _categoryServices, IProductService _productService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager)
        {
            hostingEnvironment = _hostingEnvironment;
            categoryService = _categoryServices;
            productService = _productService;
            roleManager = _roleManager;
            userManager = _userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CategoryList()
        {
            var catlist = categoryService.ListAllCategory();
            return View("ListCategory", catlist);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateCategory()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                Category categorie = new Category
                {
                    CategoryName = model.Nume

                };

                await categoryService.AddCategoryAsync(categorie);

                return RedirectToAction("CategoryList", "category");
            }

            return View("CategoryList", model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var car = await categoryService.GetCategoryByIdAsync(id);

            await categoryService.DeleteCategoryAsync(car);

            return RedirectToAction("CategoryList", "category");
        }
    }
}
