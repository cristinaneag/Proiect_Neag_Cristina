using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proiect_Neag_Cristina.Models
{
    public class Manufacturer
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        [StringLength(50)]
        public string Name { get; set; }
        public string Cui { get; set; }
        public ICollection<ManufacturedPerfumes> ManufacturedPerfumes { get; set; }

    }
}
