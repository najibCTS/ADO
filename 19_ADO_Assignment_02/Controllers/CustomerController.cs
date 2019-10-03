using _19_ADO_Assignment_02.Models;
using _19_ADO_Assignment_02.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace _19_ADO_Assignment_02.Controllers
{

    public class CustomerController : Controller
    {
        private CustomerRepository custRepo = new CustomerRepository();
        // GET: Customer
        public ActionResult Index()
        {
            return View(custRepo.GetAllCustomersDs());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View(custRepo.GetAllCustomersDs().Find(a => a.Custid == id));
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                custRepo.InsertCustomerDs(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View(custRepo.GetAllCustomersDs().Find(a => a.Custid == id));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                custRepo.UpdateCustomerDs(customer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View(custRepo.GetAllCustomersDs().Find(a => a.Custid == id));
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                custRepo.DeleteCustomerDs(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult UploadXMl()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            var file = Request.Files["uploadFile"];
            if (file == null)
            {
                ModelState.AddModelError("uploadFile", "No file specified");
            }
            custRepo.UploadXML(file);
            return RedirectToAction("UploadXMl");
        }
    }
}
