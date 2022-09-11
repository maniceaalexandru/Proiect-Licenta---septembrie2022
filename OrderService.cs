using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;
using TestFinal.Data;

namespace TestFinal.Models.Repositores
{
    public class OrderService : IOrderService
    {
        private readonly MyDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;
        private readonly IGenericRepository<Order> orderRepo;

        public OrderService(MyDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;



        }

        public async void CreateOrder(Order order)
        {

            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();
            var shoppingCartItems = _shoppingCart.GetShoppingCartItems();
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    Price = shoppingCartItem.Produs.Pret,
                    ProductId = shoppingCartItem.Produs.ProductId,
                    OrderId = order.OrderId,
                    UserID = order.UserId,
                    Nume_Produs = shoppingCartItem.Produs.ProductName

                };


                var productid = _appDbContext.Products.Where(p => p.ProductId == shoppingCartItem.Produs.ProductId).FirstOrDefault();
                var orderDetail2 = _appDbContext.OrderDetails.Where(p => p.ProductId == shoppingCartItem.Produs.ProductId).FirstOrDefault();
                productid.ProductStock -= orderDetail.Amount;

                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.SaveChanges();
        }

        public async Task DeleteById(int id)
        {
            var entity = await _appDbContext.Orders.FindAsync(id);
            _appDbContext.Orders.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Delete(Order order)
        {

            _appDbContext.Remove(order);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task<Order> GetById(string id)
        {
            return await _appDbContext.Set<Order>().FindAsync(id);
        }
    }
}
