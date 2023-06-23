﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Transistor
{
    public class TransistorUpdateModels
    {

        [Required]
        public Int64 STOCK_ID { get; set; }
        [Required]
        public String PART_NUMBER { get; set; }
        //[Required]
        //public String TYPE { get; set; }
        [Required]
        public String PACKAGING { get; set; }
        [Required]
        public double? Vce { get; set; }
        [Required]
        public Double? Ic { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }

        public String User { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
