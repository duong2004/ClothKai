using ClothKai.Entities;
using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothKai.Web.Controllers
{   
    public class CategoryController : Controller
    {
        CategoryService categoryService = new CategoryService();
        [HttpGet]
        public ActionResult Index()
        {
            var listCategory = categoryService.GetCategory();
            return View(listCategory);
        }
        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            categoryService.SaveCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var categoryID = categoryService.GetCategoryID(ID);
            if (categoryID == null)
            {
                return View("Error");
            }
            return View(categoryID);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            var categoryID = categoryService.GetCategoryID(ID);
            if (categoryID == null)
            {
                return View("Error");
            }
            return View(categoryID);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryService.DeleteCategory(category);
            return RedirectToAction("Index");
        }
    }
}