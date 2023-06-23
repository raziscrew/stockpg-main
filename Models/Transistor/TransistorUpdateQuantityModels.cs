using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Transistor
{
    public class TransistorUpdateQuantityModels
    {
        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }
    }
}
