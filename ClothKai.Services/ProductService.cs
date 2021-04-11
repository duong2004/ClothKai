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
        // Get All Product
        public List<Product> GetProduct()
        {
            using (var context = new CBContext())
            {
                return context.Products.Include(x=>x.Category).ToList();
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
