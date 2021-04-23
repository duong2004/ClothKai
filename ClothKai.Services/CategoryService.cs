using ClothKai.Database;
using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        // Get All Category
        public List<Category> GetCategory()
        {
            using (var context = new CBContext())
            {
                return context.Categories.ToList();
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
                var Category = context.Categories.Find(category.ID);
                context.Categories.Remove(Category);
                context.SaveChanges();
            }
        }
    }
}
