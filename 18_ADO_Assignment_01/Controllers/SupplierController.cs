using _18_ADO_Assignment_01.Models;
using _18_ADO_Assignment_01.Repository;
using System.Web.Mvc;

namespace _18_ADO_Assignment_01.Controllers
{
    public class SupplierController : Controller
    {
        private SupplierRepository supplierRepo = new SupplierRepository();
        // GET: Supplier
        public ActionResult Index()
        {
            return View(supplierRepo.GetAllSuppliers());
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View(supplierRepo.GetSupplierById(id));
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                supplierRepo.InsertSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View(supplierRepo.GetSupplierById(id));
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier supplier)
        {
            try
            {
                supplierRepo.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            return View(supplierRepo.GetSupplierById(id));
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                supplierRepo.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
