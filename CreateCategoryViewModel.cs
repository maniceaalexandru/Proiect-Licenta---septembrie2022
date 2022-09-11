using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "Option Name is required")]
        public string Nume { get; set; }
      
    }
}
