using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class CreateSubscriptionViewModel
    {
        public int Id { get; set; }
        public String Type { get; set; }
        public String Time { get; set; }
        public String Specification { get; set; }
        public float Price { get; set; }
    }
}
