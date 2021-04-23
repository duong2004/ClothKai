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
        //ProductService productService = new ProductService();
        //CategoryService categoryService = new CategoryService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableProduct(string s, int? pageNo)
        {
            ProductsSearchViewModel model = new ProductsSearchViewModel();
            model.PageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.Products = ProductService.Instance.GetProduct(model.PageNo);
            if (string.IsNullOrEmpty(s) == false)
            {
                model.SearchTerm = s;
                model.Products = model.Products.Where(x => x.Name != null && x.Name.ToLower().Contains(s.ToLower())).ToList();
            }
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            var categories = CategoryService.Instance.GetCategory();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(CategoriesViewModels product)
        {
            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.Category = CategoryService.Instance.GetCategoryID(product.CategoryID);
            ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("TableProduct");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            var product = ProductService.Instance.GetProductID(ID);
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            ProductService.Instance.UpdateProduct(product);
            return RedirectToAction("TableProduct");
        }
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductService.Instance.DeleteProduct(ID);
            return RedirectToAction("TableProduct");
        }
    }
}