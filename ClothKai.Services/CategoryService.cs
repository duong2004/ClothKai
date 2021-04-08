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
        // Get All Category
        public List<Category> GetCategory()
        {
            using (var context = new CBContext())
            {
                return context.Categories.ToList();
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
