using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothKai.Entities
{
    public class Product : BaseEntity
    {
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
        [Range(1, 1000000, ErrorMessage = "Price must be between 1 and 1000000")]
        public decimal Price { get; set; }
    }
}
