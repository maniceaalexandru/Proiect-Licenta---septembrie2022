using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class CreateSubcategoryViewModel
    {
        public CreateSubcategoryViewModel()
        {
            Categories = new List<string>();
        }
        public List<string> Categories { get; set; }
        public string Nume { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}
