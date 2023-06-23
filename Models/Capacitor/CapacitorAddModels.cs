using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Stocks
{
    public class CapacitorAddModels
    {

        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String CAPACITANCE { get; set; }
        [Required]
        public Double? VOLTAGE { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public Int32? IGNORE { get; set; }
        [Required]
        public string COMPONENTS_ID { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }
        public String User { get; set; }


    }
}
