using ClothKai.Entities;
using ClothKai.Services;
using ClothKai.Web.ViewModels;
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
        CategoryService categoryService = new CategoryService();
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
                listProduct = listProduct.Where(x => x.Name.ToLower().Contains(s.ToLower())).ToList();
            }
            return PartialView(listProduct);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var categories = categoryService.GetCategory();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(CategoriesViewModels product)
        {
            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.Category = categoryService.GetCategoryID(product.CategoryID);
            productService.SaveProduct(newProduct);
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