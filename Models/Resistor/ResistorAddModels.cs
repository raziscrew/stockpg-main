using System;
using System.ComponentModel.DataAnnotations;

namespace JPGStockServer.Models.Resistor
{
    public class ResistorAddModels
    {


        [Required]
        public String PART_NUMBER { get; set; }
        [Required]
        public String RESISTANCE { get; set; }
        [Required]
        public String WATT { get; set; }
        [Required]
        public Int64? QUANTITY { get; set; }
        [Required]
        public String LOCATION { get; set; }
        [Required]
        public string COMPONENTS_ID { get; set; }

        [Required]
        public String CreateDate { get; set; }
        [Required]
        public String UpdateDate { get; set; }
    }
}
