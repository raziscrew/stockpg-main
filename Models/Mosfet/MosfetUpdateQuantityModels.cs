using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Mosfet
{
    public class MosfetUpdateQuantityModels
    {
        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }
    }
}
