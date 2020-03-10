﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPrsTest.Models {
    public class Product {

        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string PartNbr { get; set; }
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [StringLength(30)]
        [Required]
        public string Unit { get; set; }
        [StringLength(255)]
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public Product() { }
    }
}
