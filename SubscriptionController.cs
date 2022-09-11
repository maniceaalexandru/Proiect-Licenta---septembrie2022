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

namespace TestFinal.Controllers
{
    public class SubscriptionController : Controller
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ISubscriptionService subscriptionService;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserManager<ApplicationUser> UserManager { get; }

        public SubscriptionController(IWebHostEnvironment _hostingEnvironment, ISubscriptionService _subscriptionService,
            RoleManager<IdentityRole> _roleManager,
               UserManager<ApplicationUser> _userManager)
        {
            hostingEnvironment = _hostingEnvironment;
            subscriptionService = _subscriptionService;
            roleManager = _roleManager;
            userManager = _userManager;
        } 
        //GET : Subscription/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        //POST : Subscription/ShowSearchResults
        public IActionResult ShowSearchResults(String SearchPhrase)
        {
            var subscriptionList = subscriptionService.GetAllWhere(SearchPhrase);
            return View("Index" , subscriptionList);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            ViewData["subscribed"] = currentUser.Subscription;

            if (User.IsInRole("Admin"))
            {
                var subscriptionList = subscriptionService.ListAllSubscription();
                return View(subscriptionList);
            }
            else
            {
                if (currentUser.Subscription == null)
                {
                    var subscriptionList = subscriptionService.ListAllSubscription();
                    return View(subscriptionList);
                }
                else
                {
                    var userSubscription = await subscriptionService.GetSubscriptionByIdAsync(currentUser.Subscription.Id);

                    List<Subscription> list = new List<Subscription>();
                    list.Add(userSubscription);

                    IEnumerable<Subscription> en = list;

                    return View(en);
                }
            }
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddSubscriptionToUserAsync(int id)
        {
            var abonament = await subscriptionService.GetSubscriptionByIdAsync(id);

            AddSubscriptionViewModel subscription = new AddSubscriptionViewModel
            {
                Id = abonament.Id,
                Type = abonament.Type,
                Time = abonament.Time,
                Price = abonament.Price,
                Specification = abonament.Specification
            };

            return View("AddToUser", subscription);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddSubscriptionToUser(AddSubscriptionViewModel model)
        {
            var abonament = await subscriptionService.GetSubscriptionByIdAsync(model.Id);
            var currentUser = await userManager.GetUserAsync(HttpContext.User);

            currentUser.Subscription = abonament;

            await userManager.UpdateAsync(currentUser);

            return RedirectToAction("Index", "subscription");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateSubscription()
        {
            CreateSubscriptionViewModel model = new CreateSubscriptionViewModel { };

            return View("add", model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Subscription subscription = new Subscription
                {
                    Type = model.Type,
                    Id = model.Id,
                    Time = model.Time,
                    Price = model.Price,
                    Specification = model.Specification
                };

                await subscriptionService.AddSubscriptionAsync(subscription);

                return RedirectToAction("Index", "subscription");
            }

            return View("add", model);
        }

        [HttpGet]
        public ViewResult List(string category)
        {
            IEnumerable<Subscription> abonament;

            abonament = subscriptionService.ListAllSubscription();
   
            return View(new SubscriptionListViewModel
            {
                Subscriptions = abonament.ToList(),
            });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var abonament = await subscriptionService.GetSubscriptionByIdAsync(id);

            await subscriptionService.DeleteSubscriptionAsync(abonament);

            return RedirectToAction("Index", "subscription");
        }

        // This action responds to HttpPost and receives EditRoleViewModel
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSubscription(EditSubscriptionViewModel model)
        {
            var abonament = await subscriptionService.GetSubscriptionByIdAsync(model.Id);

            if (abonament == null)
            {
                ViewBag.ErrorMessage = $"Subscription with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                abonament.Type = model.Type;
                abonament.Time = model.Time;
                abonament.Price = model.Price;
                abonament.Specification = model.Specification;

                await subscriptionService.UpdateSubscriptionAsync(abonament);

                return RedirectToAction("Index", "subscription");


            }
            return View("Index", model);
        }
        [HttpGet]
         [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditSubscription(int id)
        {
            var abonament = await subscriptionService.GetSubscriptionByIdAsync(id);

            if (abonament == null)
            {
                ViewBag.ErrorMessage = $"Subscription option with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditSubscriptionViewModel

            {
                Id = abonament.Id,
                Type = abonament.Type,
                Time = abonament.Time,
                Price = abonament.Price,
                Specification = abonament.Specification

            };

            return View("edit", model);
        }

       
    }
}
