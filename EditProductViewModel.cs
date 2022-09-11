using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.Models;

namespace TestFinal.ViewModels
{
    public class EditProductViewModel
    {
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
        public int Id { get; set; }
        public string Nume { get; set; }
        public float Pret { get; set; }
        public int Stoc { get; set; }
        public int CategoryId { get; set; }
        public Subcategory Subcategories { get; set; }
    }
}
