using ClothKai.Database;
using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothKai.Services
{
    public class ProductService
    {
        #region Singleton
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();
                return instance;
            }
        }       
        private static ProductService instance { get; set; }
        private ProductService()
        {
        }
        #endregion
        // Get All Products
        public List<Product> GetProduct(int pageNo)
        {
            int pageSize = 5;
            using (var context = new CBContext())
            {
                //return context.Products.OrderByDescending(x=>x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                return context.Products.Include(x => x.Category).ToList();
            }
        }
        // Get List All Products
        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }
        // Save Product
        public void SaveProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        // Get Product to ID
        public Product GetProductID(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Products.Find(ID);
            }
        }
        // Update Product
        public void UpdateProduct(Product product)
        {
            using (var context = new CBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        // Delete Product
        public void DeleteProduct(int ID)
        {
            using (var context = new CBContext())
            {
                var Product = context.Products.Find(ID);
                context.Products.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
