using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Diode
{
    public class DiodeUpdateQuantityModels
    {

        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }

    }
}
