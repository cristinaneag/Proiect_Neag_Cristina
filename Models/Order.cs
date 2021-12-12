using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int PerfumeID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Perfume Perfume { get; set; }
    }
}
