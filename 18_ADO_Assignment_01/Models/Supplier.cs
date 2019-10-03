using System.ComponentModel.DataAnnotations;

namespace _18_ADO_Assignment_01.Models
{
    public class Supplier
    {
        [Required]
        [Display(Name = "Supplier Id")]
        public int SupplierId { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [Display(Name = "Contact No")]
        [Phone]
        public long ContactNo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}