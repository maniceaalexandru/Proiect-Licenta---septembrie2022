using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;
using TestFinal.Models.Repositores;

namespace TestFinal.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService productService;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProductService _productService, ShoppingCart shoppingCart)
        {
            productService = _productService;
            _shoppingCart = shoppingCart;
        }

        [Authorize(Roles = "User")]
        public ViewResult Index()
        {
            _shoppingCart.ShoppingCartItems = _shoppingCart.GetShoppingCartItems();

            var shoppingCartViewModel = new ViewModels.ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        [Authorize(Roles = "User")]

        public RedirectToActionResult AddToShoppingCart(int productId, int amount)
        {
            var selectedProduct = productService.ListAllProduct().FirstOrDefault(c => c.ProductId == productId);
            if (selectedProduct != null)
            {
                if (amount > 0)
                {
                    _shoppingCart.AddToCart(selectedProduct, amount);
                }
                else
                {
                    _shoppingCart.AddToCart(selectedProduct, 1);
                }
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]

        public RedirectToActionResult UpdateCart(int productId, int amount)
        {
            var selectedProduct = productService.ListAllProduct().FirstOrDefault(c => c.ProductId == productId);
            if (selectedProduct != null)
            {

                _shoppingCart.Update(selectedProduct, amount);

            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]

        public RedirectToActionResult RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = productService.ListAllProduct().FirstOrDefault(c => c.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult Inncrease(int productId)
        {
            var selectedProduct = productService.ListAllProduct().FirstOrDefault(c => c.ProductId == productId);

            if (selectedProduct != null)
            {
                _shoppingCart.Increase(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "User")]

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
            return RedirectToAction("Index");
        }
    }
}
