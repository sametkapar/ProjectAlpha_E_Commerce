using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAlpha.Model
{
    internal class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public  string  CategoryName { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
