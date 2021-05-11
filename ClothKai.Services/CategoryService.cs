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
    public class CategoryService
    {
        #region Singleton
        public static CategoryService Instance
        {
            get
            {
                if (instance == null) instance = new CategoryService();
                return instance;
            }
        }
        private static CategoryService instance { get; set; }
        private CategoryService()
        {
        }
        #endregion
        public List<Category> GetCategory()
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.ToList();
                return category;
            }
        }
        // Get All Category to s
        public List<Category> GetCategoryToSearch(string s, int pageNo)
        {
            using (var context = new CBContext())
            {
                var pageSize = int.Parse(ConfigrutionService.Instance.GetConfig("PageSize").Value);
                if (!string.IsNullOrEmpty(s))
                {
                    return context.Categories
                        .Where(x => x.Name.ToLower().Contains(s.ToLower()))
                        .OrderByDescending(x => x.ID)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                return context.Categories
                    .OrderByDescending(x => x.ID)
                    .Skip((pageNo - 1)*pageSize)
                    .Take(pageSize)
                    .Include(x => x.Products)
                    .ToList();
            }
        }
        // Count Category to search
        public int CountToCategory(string s)
        {
            using (var context = new CBContext())
            {
                var category = context.Categories.ToList();
                if (!string.IsNullOrEmpty(s))
                {
                    category = category.Where(x => x.Name.ToLower().Contains(s.ToLower())).ToList();
                }
                return (int)(category.Count());
            }
        }
        // Get Feature Categories Home
        public List<Category> GetFeatureCategory()
        {
            using (var context = new CBContext())
            {
                return context.Categories.Where(x=>x.isFeature && x.ImageURL != null).OrderByDescending(x=>x.ID).Take(4).ToList();
            }
        }
        // Save Category
        public void SaveCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }
        // Get Category to ID
        public Category GetCategoryID(int ID)
        {
            using (var context = new CBContext())
            {
                return context.Categories.Find(ID);
            }
        }
        // Update Category
        public void UpdateCategory(Category category)
        {
            using (var context = new CBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        // Delete Category
        public void DeleteCategory(Category category)
        {
            using (var context = new CBContext())
            {
                var Category = context.Categories
                    .Where(x=>x.ID == category.ID)
                    .Include(x=>x.Products)
                    .FirstOrDefault();
                context.Products.RemoveRange(Category.Products);
                context.Categories.Remove(Category);
                context.SaveChanges();
            }
        }
    }
}
