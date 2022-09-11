using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models
{
    public class Subcategory
    {

        [Key]
        public int SubCategorieID { get; set; }
        public string Nume { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual Category Categories { get; internal set; }
    }

}

