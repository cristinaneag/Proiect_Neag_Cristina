using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Models.StoreViewModels
{
    public class ManufacturerIndexData
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Perfume> Perfumes { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
