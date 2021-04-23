using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothKai.Web.ViewModels;

namespace ClothKai.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
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