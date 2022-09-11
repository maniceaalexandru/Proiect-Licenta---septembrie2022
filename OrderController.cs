using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;
using TestFinal.Models;
using TestFinal.ViewModels;
using System.Security.Claims;
using TestFinal.Models.Repositores;
using TestFinal.Data;



namespace TestFinal.Controllers
{
    //[Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly MyDbContext context;
        private readonly UserManager<ApplicationUser> _userManager;


        public OrderController(IOrderService orderRepository, ShoppingCart shoppingCart, MyDbContext _context, UserManager<ApplicationUser> userManager)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            context = _context;
            _userManager = userManager;
        }
        [Authorize(Roles = "User")]

        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> List()
        {
            return View(await context.Orders.ToListAsync());
        }
        [Authorize(Roles = "User")]

        public IActionResult ListOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            return View(context.OrderDetails.Where(c => c.UserID.Contains(userId)).ToList());

        }
        public async Task<IActionResult> Info(int id)
        {
            var order = await context.OrderDetails.FindAsync(id);
            ViewBag.Nume = order.Order.FirstName;
            ViewBag.LastName = order.Order.LastName;
            ViewBag.Produs = order.Nume_Produs;
            ViewData["id"] = order.OrderId;
            return (IActionResult)order;

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await context.Orders.FindAsync(id);

            await _orderRepository.Delete(order);

            return RedirectToAction("List", "order");
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult Checkout(Models.Order order)
        {
           

            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = User.Identity.Name;
                order.UserId = userId;
                _orderRepository.CreateOrder(order);
                _shoppingCart.ClearCart();

                MailMessage mail = new MailMessage();
                mail.To.Add(new MailAddress("antonieraduemilian@gmail.com"));
                mail.From = new MailAddress("antonieraduemilian@gmail.com");
                mail.Subject = "Magazin E-Thermal";
                mail.Body = "Comanda noua de la " + user ;

                SmtpClient smtp = new SmtpClient();
                smtp.UseDefaultCredentials = false;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("antonieraduemilian@gmail.com", "qxsutyzjblnpdscm");
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}