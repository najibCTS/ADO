using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    public class EmployeeLeaveController : Controller
    {
        private LeaveRepository leaveRepo = new LeaveRepository();
        private EmployeesRepository empRep = new EmployeesRepository();

        public ActionResult Index(Employees emplolyees)
        {
            List<LeaveRequest> leaveReqList = leaveRepo.GetEmployeeLeaves(emplolyees);
            Session["UserName"] = leaveReqList[0].Employees.UserName; 
            return View(leaveReqList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LeaveRequest  leaveRequest)
        {
            try
            {
                leaveRepo.ApplyLeave(leaveRequest, Session["UserName"].ToString());
                return RedirectToAction("Index", leaveRequest.Employees);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ManagerIndex(Employees emplolyees)
        {
            List<LeaveRequest> leaveReqList = leaveRepo.GetReporteesLeaves(emplolyees);
            Session["ManagerUserName"] = emplolyees.UserName;
            return View(leaveReqList);
        }

        [HttpPost]
        public ActionResult Edit(LeaveRequest leaveRequest)
        {
            try
            {
                leaveRepo.ApproveLeave(leaveRequest);
                Employees emp = empRep.GetEmployeesByUsername(Session["ManagerUserName"].ToString());
                return RedirectToAction("ManagerIndex", emp);
            }
            catch
            {
                return View();
            }
        }
    }
}
