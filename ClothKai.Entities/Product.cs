using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothKai.Entities
{
    public class Product : BaseEntity
    {
        public virtual Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
