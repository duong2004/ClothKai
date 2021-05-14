using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothKai.Web.ViewModels;
using ClothKai.Web.Code;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using ClothKai.Entities;

namespace ClothKai.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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
            model.CategoryID = categoryID;
            model.searchTerm = searchTerm;
            model.sortBy = sortBy;
            return PartialView(model);
        }
        // GET: Shop
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();
            var CartProductsCookie = Request.Cookies["CartProducts"];
            if (CartProductsCookie != null && !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                //var productsID = CartProductsCookie.Value;
                //var ids = productsID.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();
                model.ProductIDs = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductService.Instance.GetProducts(model.ProductIDs);
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }
        public JsonResult PlaceOrder(string productIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantitys = productIDs.Split('-').Select(x => int.Parse(x)).ToList();
                var pIDs = productIDs.Split(new char[] { '-' }).Select(x => int.Parse(x)).Distinct().ToList();
                var boughtProducts = ProductService.Instance.GetProducts(pIDs);
                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Pending";
                newOrder.TotalAmout = boughtProducts.Sum(x => x.Price * productQuantitys.Where(product => product == x.ID).Count());
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.ID, Quantity = productQuantitys.Where(product => product == x.ID).Count() }));
                var rowEffected = ShopService.Instance.SaveOrder(newOrder);
                result.Data = new { Success = true,Rows = rowEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
}