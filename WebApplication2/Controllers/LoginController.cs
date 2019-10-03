using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        private EmployeesRepository empRepo = new EmployeesRepository();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                Employees employee = empRepo.GetEmployeesByUsername(login.UserName);
                if (login.Password.Equals(employee.Password))
                {
                    switch (employee.Role)
                    {
                        case "Employee":
                            return RedirectToAction("Index", "EmployeeLeave", employee);
                        case "Manager":
                            return RedirectToAction("ManagerIndex", "EmployeeLeave", employee);
                    }
                }
            }
            return View();
        }
    }
}