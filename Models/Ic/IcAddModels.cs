﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Ic
{
    public class IcAddModels
    {

        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String PACKAGING { get; set; }
        [Required]
        public String TYPE { get; set; }
        //[Required]
        //public String SERIES { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }

        [Required]
        public string COMPONENTS_ID { get; set; }
        public String User { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
