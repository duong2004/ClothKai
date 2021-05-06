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
        // Get Products by search in Shop.
        public List<Product> SearchProducts(string searchTerm, int? minnimumPrice, int? maxnimumPrice, int? categoryID, int? sortBy, int pageNo)
        {
            int pageSize = Int32.Parse(ConfigrutionService.Instance.GetConfig("ShopPageSize").Value);
            using (var context = new CBContext())
            {
                var products = new List<Product>();
                products = context.Products.ToList();
                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minnimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minnimumPrice.Value).ToList();
                }
                if (maxnimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxnimumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value) 
                    {
                        case 1:
                            products = products.OrderBy(x => x.ID).ToList();
                            break;
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }

                }
                return products.Skip((pageNo - 1)*pageSize).Take(pageSize).ToList();
            }
        }
        //Search Products Count Shop.
        public int SearchProductsCount(string searchTerm, int? minnimumPrice, int? maxnimumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new CBContext())
            {
                var products = new List<Product>();
                products = context.Products.ToList();
                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.CategoryID == categoryID.Value).ToList();
                }
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minnimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minnimumPrice.Value).ToList();
                }
                if (maxnimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maxnimumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 1:
                            products = products.OrderBy(x => x.ID).ToList();
                            break;
                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                    }

                }
                return products.Count();
            }
        }
        // Get Latest Products
        public int GetMaximumPrice()
        {
            using (var context = new CBContext())
            {
                return (int)(context.Products.Max(x => x.Price));
            }
        }
        // Get All Products
        public List<Product> GetProduct(string s, int pageNo)
        {
            int pageSize = Int32.Parse(ConfigrutionService.Instance.GetConfig("PageSize").Value);
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(s))
                {
                    return context.Products
                        .Where(x => x.Name != null && x.Name.ToLower().Contains(s.ToLower()))
                        .OrderByDescending(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Category)
                        .ToList();
                }
                else
                {
                    return context.Products
                    .OrderByDescending(x => x.ID)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .ToList();
                }
            }
        }
        // Get Latest Products
        public List<Product> GetLatestProducts(int numberOfProducts)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();
            }
        }
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.OrderByDescending(x => x.ID).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        // Get Products by Category.
        public List<Product> GetProductsByCategory(int CategoryID, int pageSize)
        {
            using (var context = new CBContext())
            {
                return context.Products.Where(x => x.CategoryID == CategoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();
            }
        }
        // Get Count Number Product
        public int GetProductCount(string s)
        {
            using (var context = new CBContext())
            {
                if (!string.IsNullOrEmpty(s))
                {
                    return context.Products
                        .Where(x => x.Name != null && x.Name.ToLower().Contains(s.ToLower()))
                        .Count();
                }
                else
                {
                    return context.Products.Count();
                }
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
