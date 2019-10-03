using System.ComponentModel.DataAnnotations;

namespace _18_ADO_Assignment_01.Models
{
    public class Product
    {
        [Required]
        [Display(Name = "Product Id")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
    }
}