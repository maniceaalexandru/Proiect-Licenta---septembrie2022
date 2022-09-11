using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.ViewModels
{
    public class EditSubcategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string Nume { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
