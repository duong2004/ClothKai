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
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TableProduct(string s, int? pageNo)
        {
            ProductsSearchViewModel model = new ProductsSearchViewModel();
            model.SearchTerm = s;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalItem = ProductService.Instance.GetProductCount(s);
            model.Products = ProductService.Instance.GetProduct(s,pageNo.Value);
            model.Pager = new Pager(totalItem,pageNo);
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();
            model.AvailableCategories = CategoryService.Instance.GetCategory();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel product)
        {
            var newProduct = new Product();
            newProduct.Name = product.Name;
            newProduct.Description = product.Description;
            newProduct.Price = product.Price;
            newProduct.Category = CategoryService.Instance.GetCategoryID(product.CategoryID);
            newProduct.ImageURL = product.ImageURL;
            ProductService.Instance.SaveProduct(newProduct);
            return RedirectToAction("TableProduct");
        }
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();
            var product = ProductService.Instance.GetProductID(ID);
            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.CategoryID;
            model.ImageURL = product.ImageURL;
            model.AvailableCategories = CategoryService.Instance.GetCategory();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModel product)
        {
            var editProduct = ProductService.Instance.GetProductID(product.ID);
            editProduct.Name = product.Name;
            editProduct.Description = product.Description;
            editProduct.Price = product.Price;
            //editProduct.Category = null;
            editProduct.CategoryID = product.CategoryID;
            if (!string.IsNullOrEmpty(product.ImageURL))
            {
                editProduct.ImageURL = product.ImageURL;
            }
            ProductService.Instance.UpdateProduct(editProduct);
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