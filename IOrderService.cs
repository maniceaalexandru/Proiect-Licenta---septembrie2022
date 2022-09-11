using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
        public Task DeleteById(int id);
        public Task Delete(Order order);
        public Task<Order> GetById(string id);
    }
}
