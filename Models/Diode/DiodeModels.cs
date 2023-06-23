using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Diode
{
    public class DiodeModels
    {
        [Required]
        public Int64 STOCK_ID { get; set; }
        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public Double? VOLTAGE { get; set; }
        [Required]
        public Double? AMPERE { get; set; }
        [Required]
        public Double? WATT { get; set; }
        [Required]
        public String TYPE { get; set; }
        [Required]
        public String PACKAGING { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        public String User { get; set; }
    }
}
