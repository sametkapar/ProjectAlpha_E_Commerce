using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAlpha.Model
{
    internal class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
}
