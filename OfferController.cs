using TestFinal.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models.Repositores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using TestFinal.ViewModels;
using TestFinal.Data;
using System.IO;

namespace TestFinal.Controllers
{
    public class OfferController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IOfferService offerService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly MyDbContext context;
        public UserManager<ApplicationUser> UserManager { get; }

        public OfferController(IWebHostEnvironment _hostingEnvironment, IOfferService _offerService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager,MyDbContext _context )
        {
            hostingEnvironment = _hostingEnvironment;
            offerService = _offerService;
            roleManager = _roleManager;
            userManager = _userManager;
            context = _context;
        }

        [AllowAnonymous]
        [HttpGet]

        public IActionResult Index()
        {
            var clasa = offerService.ListAllOfferWith();
            return View("Index", clasa);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateOffer()
        {
            CreateOfferViewModel model = new CreateOfferViewModel { };

            return View("add", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOffer(CreateOfferViewModel model)
        {
            if (ModelState.IsValid)
            {

                Offer offer = new Offer
                {
                    Title = model.Title,
                    Id = model.Id,
                    Description = model.Description,
                };

                await offerService.AddOfferAsync(offer);

                return RedirectToAction("Index", "offer");
            }

            return View("add", model);
        }
        [HttpGet]
        public ViewResult List(string category)
        {
            IEnumerable<Offer> clasa;

            clasa = offerService.ListAllOffer();

            return View(new OffersListViewModel
            {
                Offers = clasa.ToList(),
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var clasa = await offerService.GetOfferByIdAsync(id);

            await offerService.DeleteOfferAsync(clasa);

            return RedirectToAction("Index", "offer");
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditOffer(EditOfferViewModel model)
        {
            var clasa = await offerService.GetOfferByIdAsync(model.Id);

            if (clasa == null)
            {
                ViewBag.ErrorMessage = $"Offer with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                clasa.Title = model.Title;
                clasa.Description = model.Description;

                await offerService.UpdateOfferAsync(clasa);

                return RedirectToAction("Index", "offer");


            }
            return View("Index", model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditOffer(int id)
        {
            var clasa = await offerService.GetOfferByIdAsync(id);

            if (clasa == null)
            {
                ViewBag.ErrorMessage = $"Car option with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditOfferViewModel

            {
                Id = clasa.Id,
                Title = clasa.Title,
                Description = clasa.Description,
            };

            return View("edit", model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
                return NotFound();



            var offer = await offerService.GetOfferByIdAsync(id);

            if (offer == null)
                return NotFound();
            

            var user = await userManager.GetUserAsync(HttpContext.User);

            var offermodel = new OfferViewModel()
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,      
                User = user,
            };

            return View(offermodel);
        }


    }
}
