using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Models
{
    public class Perfume
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
