using ClothKai.Entities;
using ClothKai.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothKai.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableProduct(string s)
        {
            var listProduct = productService.GetProduct();
            if (s != null)
            {
                listProduct = listProduct.Where(x => x.Name.Contains(s)).ToList();
            }
            return PartialView(listProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            productService.SaveProduct(product);
            return RedirectToAction("TableProduct");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = productService.GetProductID(ID);
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productService.UpdateProduct(product);
            return RedirectToAction("TableProduct");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            productService.DeleteProduct(ID);
            return RedirectToAction("TableProduct");
        }
    }
}