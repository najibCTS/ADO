using System;
using System.ComponentModel.DataAnnotations;

namespace _19_ADO_Assignment_02.Models
{
    public class Customer
    {
        public int Custid { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Custname { get; set; }
        [Display(Name = "Address")]
        public string CustAddress { get; set; }
        [Display(Name = "Date Of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public long Salary { get; set; }
    }
}