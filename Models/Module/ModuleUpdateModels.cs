using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Module
{
    public class ModuleUpdateModels
    {
        [Required]
        public Int64 STOCK_ID { get; set; }
        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String BRAND { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }

        public String User { get; set; }

        [Required]
        public String UpdateDate { get; set; }
    }
}
