using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Optocoupler
{
    public class OptocouplerUpdateModels
    {

        [Required]
        public Int64 STOCK_ID { get; set; }
        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String PACKAGING { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }
        [Required]
        public String LOCATION { get; set; }
        public String User { get; set; }

        [Required]
        public DateTime UpdateDate { get; set; }
    }
}
