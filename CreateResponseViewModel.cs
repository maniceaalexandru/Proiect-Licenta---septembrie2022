using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class CreateResponseViewModel
    {
        public int Id { get; set; }
        public int ContactId { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
