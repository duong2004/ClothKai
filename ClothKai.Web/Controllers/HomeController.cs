using ClothKai.Services;
using ClothKai.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothKai.Web.Controllers
{
    public class HomeController : Controller
    {
        CategoryService categoryService = new CategoryService();
        HomeViewModels model = new HomeViewModels();
        public ActionResult Index()
        {           
            model.FeatureCategories = categoryService.GetFeatureCategory();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}