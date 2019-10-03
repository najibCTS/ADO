using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }
        public int RequestorId { get; set; }
        [Display(Name ="From")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime FromDate { get; set; }
        [Display(Name = "To")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime ToDate { get; set; }
        [Display(Name = "Total Days")]
        [Required]
        public int NoOfDays { get; set; }
        public string Status { get; set; }
        [Required]
        public string Reason { get; set; }
        [Display(Name = "Contact No")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string ContactNo { get; set; }
        public int ApproverId { get; set; }

        public Employees Employees { get; set; }
    }
}