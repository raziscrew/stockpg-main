using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Module
{
    public class ModuleAddModels
    {


        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String BRAND { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }
        [Required]
        public string COMPONENTS_ID { get; set; }
        public String User { get; set; }

        [Required]
        public String CreateDate { get; set; }
        [Required]
        public String UpdateDate { get; set; }
    }
}
