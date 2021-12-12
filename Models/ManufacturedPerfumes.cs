using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect_Neag_Cristina.Models
{
    public class ManufacturedPerfumes
    {
        public int ManufacturerID { get; set; }
        public int PerfumeID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public Perfume Perfume { get; set; }
    }
}
