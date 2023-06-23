using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Optocoupler
{
    public class OptocouplerUpdateQuantityModels
    {
        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }
    }
}
