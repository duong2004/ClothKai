using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothKai.Web.ViewModels;
using ClothKai.Web.Code;

namespace ClothKai.Web.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(string searchTerm, int? minnimumPrice, int? maxnimumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            ShopViewModel model = new ShopViewModel();
            model.Categories = CategoryService.Instance.GetCategory();
            model.MaxnimumPrice = ProductService.Instance.GetMaximumPrice();
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minnimumPrice, maxnimumPrice, categoryID, sortBy, pageNo.Value);
            model.sortBy = sortBy;
            model.CategoryID = categoryID;
            model.searchTerm = searchTerm;
            var totalProducts = ProductService.Instance.SearchProductsCount(searchTerm, minnimumPrice, maxnimumPrice, categoryID, sortBy);
            int pageSize = int.Parse(ConfigrutionService.Instance.GetConfig("ShopPageSize").Value);
            model.Pager = new Pager(totalProducts,pageNo,pageSize);
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minnimumPrice, int? maxnimumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            FilterProductsViewModel model = new FilterProductsViewModel();
            model.Products = ProductService.Instance.SearchProducts(searchTerm, minnimumPrice, maxnimumPrice, categoryID, sortBy, pageNo.Value);
            var totalProducts = ProductService.Instance.SearchProductsCount(searchTerm, minnimumPrice, maxnimumPrice, categoryID, sortBy);
            int pageSize = int.Parse(ConfigrutionService.Instance.GetConfig("ShopPageSize").Value);
            model.Pager = new Pager(totalProducts, pageNo, pageSize);
            return PartialView(model);
        }
        // GET: Shop
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null)
            {
                //var productsID = CartProductsCookie.Value;
                //var ids = productsID.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();
                model.ProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductService.Instance.GetProducts(model.ProductIDs);
            }
            return View(model);
        }
    }
}