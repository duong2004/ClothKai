using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothKai.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50), MinLength(8)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}
