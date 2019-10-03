using _18_ADO_Assignment_01.Models;
using _18_ADO_Assignment_01.Repository;
using System.Web.Mvc;

namespace _18_ADO_Assignment_01.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository productRepo = new ProductRepository();
        // GET: Product
        public ActionResult Index()
        {
            return View(productRepo.GetAllProducts());
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View(productRepo.GetProductById(id));
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                productRepo.InsertProduct(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View(productRepo.GetProductById(id));
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                productRepo.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View(productRepo.GetProductById(id));
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                productRepo.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
