using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public virtual Product Produs { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public virtual Order Order { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserID { get; set; }
        public string Nume_Produs { get; set; }
    }
}
