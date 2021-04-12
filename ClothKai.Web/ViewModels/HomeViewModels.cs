using ClothKai.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothKai.Web.ViewModels
{
    public class HomeViewModels
    {
        public List<Category> FeatureCategories { get; set; }
        public List<Product> FeatureProducts { get; set; }
    }
}