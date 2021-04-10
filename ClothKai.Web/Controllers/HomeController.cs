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
        public ActionResult Index()
        {
            HomeViewModels model = new HomeViewModels();
            model.categories = categoryService.GetCategory();
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