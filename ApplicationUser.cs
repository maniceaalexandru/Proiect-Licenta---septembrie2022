using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestFinal.Models
{
    public class ApplicationUser:IdentityUser
    {   
        public String Oras { get; set; }
        public string ProfilePicture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NumberPhone { get; set; }
        public virtual Subscription Subscription { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}
