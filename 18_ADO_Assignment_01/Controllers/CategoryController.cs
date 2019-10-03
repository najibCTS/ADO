using _18_ADO_Assignment_01.Models;
using _18_ADO_Assignment_01.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _18_ADO_Assignment_01.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryRepository categoryRepo = new CategoryRepository();

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(Category category)
        {
            return View("Search", categoryRepo.SearchCategory(category));
        }

        public ActionResult Edit(string code)
        {
            return View(categoryRepo.GetCategoryByCode(code));
        }
        [HttpPost]
        public ActionResult Edit(string code, Category category)
        {
            categoryRepo.UpdateCategory(category);
            return RedirectToAction("Search");
        }
    }
}