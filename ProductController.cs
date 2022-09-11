
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Data;
using TestFinal.Models;
using TestFinal.Models.Repositores;
using TestFinal.ViewModels;

namespace TestFinal.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;

        private readonly ICategoryServices categoryService;
        private readonly ISubcategoryServices subcategoryService;
        private readonly IProductService productService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly MyDbContext context;
        public UserManager<ApplicationUser> UserManager { get; }

        public ProductController(IWebHostEnvironment _hostingEnvironment, ICategoryServices _categoryServices, IProductService _productService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager, ISubcategoryServices _subcategoryService, MyDbContext _context)
        {
            hostingEnvironment = _hostingEnvironment;
            categoryService = _categoryServices;
            productService = _productService;
            roleManager = _roleManager;
            userManager = _userManager;
            subcategoryService = _subcategoryService;
            context = _context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult ListProduct()
        {
            var product = productService.ListAllProductWith();
            return View("ListProduct", product);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateProduct()
        {
            CreateProductViewModel model = new CreateProductViewModel { };
            foreach (Subcategory cat in subcategoryService.ListAllSubCategory())
            {
                model.Subcategories.Add(cat.Nume);
            }

            return View("CreateProduct", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel model)
        {
            var cat = subcategoryService.GetSubCategoryByName(model.SubCategory);

            if (ModelState.IsValid)
            {
                string uniquePhotoFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fs);
                    }
                }
                string uniquePhotoFileName1 = null;
                if (model.Photo1 != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName1 = Guid.NewGuid().ToString() + "_" + model.Photo1.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName1);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo1.CopyTo(fs);
                    }
                }

             
                string uniquePhotoFileName2 = null;
                if (model.Photo2 != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName2 = Guid.NewGuid().ToString() + "_" + model.Photo2.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName2);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo2.CopyTo(fs);
                    }
                }
           

                Product produs = new Product
                {
                    ProductName = model.Nume,
                    ProductId = model.ProdusId,
                    Pret = model.Pret,
                    ProductPicture = uniquePhotoFileName,
                    ProductPicture2 = uniquePhotoFileName1,
                    ProductPicture3 = uniquePhotoFileName2,
                    Subcategorie = cat[0],
                    ProductStock = model.Stock
                  
                };

                await productService.AddProductAsync(produs);

                return RedirectToAction("ListProduct", "product");
            }

            return View("CreateProduct", model);
        }

        [AllowAnonymous]
        public ViewResult ListProducts(string subcategory)
        {
            IEnumerable<Product> product;
            string currentSubCategory;

            if (string.IsNullOrEmpty(subcategory))
            {
                product = productService.ListAllProduct();
                currentSubCategory = "All Product";
            }
            else
            {
                product = productService.ListAllProduct().Where(c => c.Subcategorie.Nume == subcategory);

                currentSubCategory = subcategoryService.ListAllSubCategory().FirstOrDefault(c => c.Nume == subcategory)?.Nume;
            }

            return View(product.ToList());
        }


        [Authorize(Roles = "Admin")]
        public RedirectToActionResult UpdateStock(int productId, int amount)
        {
            var selectedProduct = productService.ListAllProduct().FirstOrDefault(c => c.ProductId == productId);
            if (selectedProduct != null)
            {

                selectedProduct.ProductStock = amount;

            }
            context.SaveChanges();

            return RedirectToAction("ListProducts" , "product");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            await productService.DeleteProductAsync(product);

            return RedirectToAction("ListProduct", "product");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"Car option with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditProductViewModel
            {
                Id = product.ProductId,
                Nume = product.ProductName,
                Pret = product.Pret,
                Subcategories = product.Subcategorie
            };

            return View("EditProduct", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditProduct(EditProductViewModel model)
        {
            var product = await productService.GetProductByIdAsync(model.Id);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"Product with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                string uniquePhotoFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniquePhotoFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniquePhotoFileName);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fs);
                    }
                }
                product.ProductName = model.Nume;
                product.Pret = model.Pret;
                product.ProductPicture = uniquePhotoFileName;

                await productService.UpdateProductAsync(product);

                return RedirectToAction("ListProduct", "product");

            }
            return View("ListProduct", model);
        }


       [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();

            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
