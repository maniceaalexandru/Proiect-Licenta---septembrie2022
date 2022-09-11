using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;
using TestFinal.Models.Repositores;
using TestFinal.ViewModels;

namespace TestFinal.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly ISubcategoryServices subcategoryService;
        private readonly ICategoryServices categoryService;
        private readonly IProductService productService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserManager<ApplicationUser> UserManager { get; }

        public SubcategoryController(IWebHostEnvironment _hostingEnvironment, ICategoryServices _categoryService, ISubcategoryServices _subcategoryService, IProductService _productService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager)
        {
            hostingEnvironment = _hostingEnvironment;
            subcategoryService = _subcategoryService;
            productService = _productService;
            roleManager = _roleManager;
            userManager = _userManager;
            categoryService = _categoryService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult SubCategoryList()
        {
            var sublist = subcategoryService.ListAllSubCategory();
            return View("ListSubcategory", sublist);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateSubcategory()
        {
            CreateSubcategoryViewModel model = new CreateSubcategoryViewModel { };
            foreach (Category cat in categoryService.ListAllCategory())
            {
                model.Categories.Add(cat.CategoryName);
            }

            return View("CreateSubcategory", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSubcategory(CreateSubcategoryViewModel model)
        {
            var cat = categoryService.GetCategoryByName(model.Category);
            if (ModelState.IsValid)
            {
               
                Subcategory subcategorie = new Subcategory
                {
                    Nume = model.Nume,
                    Categories = cat[0]


                };

                await subcategoryService.AddSubCategoryAsync(subcategorie);

                return RedirectToAction("SubCategoryList", "subcategory");
            }

            return View("ListSubcategory", model);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var subcat = await subcategoryService.GetSubCategoryByIdAsync(id);

            await subcategoryService.DeleteSubCategoryAsync(subcat);

            return RedirectToAction("SubCategoryList", "subcategory");
        }
    }
}
