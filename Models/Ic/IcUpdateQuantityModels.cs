using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Ic
{
    public class IcUpdateQuantityModels
    {
        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }
    }
}
