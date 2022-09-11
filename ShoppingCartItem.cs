using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public string ShoppingCartId { get; set; }
        public virtual Product Produs { get; set; }
        public int Amount { get; set; }
    }
}
