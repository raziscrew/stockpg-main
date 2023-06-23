using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Module
{
    public class ModuleUpdateQuantityModels
    {
        [Required]
        public Int32 STOCK_ID { get; set; }

        [Required]
        public Int64? QUANTITY { get; set; }
    }
}
