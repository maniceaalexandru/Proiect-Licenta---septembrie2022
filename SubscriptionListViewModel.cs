using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;

namespace TestFinal.ViewModels
{
    public class SubscriptionListViewModel
    {
        public IEnumerable<Subscription> Subscriptions { get; set; }
    }
}
