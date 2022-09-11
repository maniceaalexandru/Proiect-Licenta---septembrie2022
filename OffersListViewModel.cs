using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;

namespace TestFinal.ViewModels
{
    public class OffersListViewModel
    {
        public IEnumerable<Offer> Offers { get; set; }
    }
}
