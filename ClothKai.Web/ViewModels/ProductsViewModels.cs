using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClothKai.Web.ViewModels
{
    public class ProductsSearchViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
    public class NewProductViewModel
    {
        [Required]
        [MaxLength(50), MinLength(8)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [Range(1, 1000000, ErrorMessage = "Price must be between 1 and 1000000")]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
    public class EditProductViewModel 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
    }


}