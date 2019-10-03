using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace _18_ADO_Assignment_01.Models
{
    public class Category
    {
        [Display(Name = "Category Code")]
        public string category_code { get; set; }
        [Display(Name = "Category Name")]
        public string category_name { get; set; }
        [Display(Name = "Division")]
        public string division { get; set; }
        [Display(Name = "Region")]
        public string region { get; set; }
        [Display(Name = "Supplier Id")]
        public int supplier_id { get; set; }
        [Display(Name = "Supplier Name")]
        public string supplier_name { get; set; }

        public List<Category> CategoryResults { get; set; }
    }
}