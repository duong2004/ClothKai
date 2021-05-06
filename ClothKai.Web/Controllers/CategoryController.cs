using ClothKai.Entities;
using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothKai.Web.ViewModels;

namespace ClothKai.Web.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string s, int? pageNo)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalCategory = CategoryService.Instance.CountToCategory(s);
            model.Categories = CategoryService.Instance.GetCategoryToSearch(s, pageNo.Value);
            model.SearchTerm = s;
            int pageSize = int.Parse(ConfigrutionService.Instance.GetConfig("PageSize").Value);
            model.Pager = new Pager(totalCategory, pageNo, pageSize);
            return PartialView(model);
        }
        // GET: Category/Create
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var category = new Category();
            category.Name = model.Name;
            category.Description = model.Description;
            category.ImageURL = model.ImageURL;
            category.isFeature = model.isFeature;
            CategoryService.Instance.SaveCategory(category);
            return RedirectToAction("CategoryTable");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var categoryID = CategoryService.Instance.GetCategoryID(ID);
            model.ID = ID;
            model.Name = categoryID.Name;
            model.Description = categoryID.Description;
            model.ImageURL = categoryID.ImageURL;
            model.isFeatuer = categoryID.isFeature;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var category = new Category();
            category.ID = model.ID;
            category.Name = model.Name;
            category.ImageURL = model.ImageURL;
            category.Description = model.Description;
            category.isFeature = model.isFeatuer;
            CategoryService.Instance.UpdateCategory(category);
            return RedirectToAction("CategoryTable");
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoryService.Instance.DeleteCategory(category);
            return RedirectToAction("CategoryTable");
        }
    }
}