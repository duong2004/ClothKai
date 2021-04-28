using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothKai.Web.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> ProductIDs { get; set; }
    }
    public class ShopViewModel
    {
        public int MaxnimumPrice { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
        public int? sortBy { get; set; }
        public int? CategoryID { get; set; }
    }
}