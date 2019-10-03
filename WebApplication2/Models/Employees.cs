using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Employees
    {
        public int EmpId { get; set; }
        [Display(Name ="First Name")]
        public string EmpFName { get; set; }
        [Display(Name = "Last Name")]
        public string EmpLName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int ManagerId { get; set; }
        public int LeaveBalance { get; set; }
    }
}